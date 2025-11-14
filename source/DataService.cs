using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PersonalFinancialManager.source.TryGetReceiptsResultUnit;
using static PersonalFinancialManager.source.JsonServerClass;
using static PersonalFinancialManager.source.Database;
using PersonalFinancialManager.source.Forms;

namespace PersonalFinancialManager.source
{
    public class DataService : IDisposable
    {
        private static FTS? fts = null;
        private static Database? database = null;


        private static DataService? singleInstance = null;


        public class StatisticDataUnit
        {
            public double Value = 0;
            public DateTime date;
        }

        private DataService() { }

        public static DataService Fabric(out bool isUserAuthorizated)
        {
            isUserAuthorizated = true;

            if (singleInstance != null) return singleInstance;

            singleInstance = new DataService();

            database = Database.Fabric();

            ProductCategory.SetAllCategories(ref database);

            isUserAuthorizated = database.IsUserAuthorizated(out string? userToken);
            fts = FTS.Fabric(userToken);

            return singleInstance;
        }

        public void SetDatabaseCurrentConditionTree(SearchConditionNode condition, Database.EntityType type)
        {
            database.SetCurrentConditionString(condition, type);
        }

        public void ClearDatabaseSortConditions(Database.EntityType currentType)
        {
            database.ClearSortConditions(currentType);
        }

        public Database.EntityType GetDatabaseCurrentEntityType()
        {
            return database.CurrentEntityType;
        }

        public void Dispose()
        {
            singleInstance = null;
            database?.Dispose();
            fts?.Dispose();
            database = null;
            fts = null;
        }

        public void SetUserToken(string token)
        {
            fts.UpdateUserToken(token);
            database.SetUserData(token);
        }

        /// <summary>
        /// Before call this function, you need to authorizate user, if he is not authorizate yet.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TryGetReceiptsResultUnit>> GetReceiptsFromQRCodes(string[] files)
        {
            List<TryGetReceiptsResultUnit> fTSResult = new List<TryGetReceiptsResultUnit>();
            List<QRCodeData> listOfQRData = new List<QRCodeData>();

            QRCodeData? qRCodeData;

            for (int i = 0; i < files.Length; i++)
            {
                TryGetReceiptsResultUnit unit;

                if ((qRCodeData = await QRCodeData.ParseDataFromQRImageAsync(files[i])) != null)
                {
                    if (listOfQRData.Contains(qRCodeData))
                        continue;

                    listOfQRData.Add(qRCodeData);
                    unit = await GetReceiptFromQRData(qRCodeData);
                    if (unit.Fail != null)
                        unit.Fail.FileName = files[i];
                }
                else
                {
                    unit = new TryGetReceiptsResultUnit();
                    unit.Fail = new FailData();
                    unit.Fail.FileName = files[i];
                }

                fTSResult.Add(unit);
            }

            return fTSResult;
        }


        public async Task<TryGetReceiptsResultUnit> GetReceiptFromDataString(string ftsData)
        {
            if (QRCodeData.TryParseQRCodeData(ftsData, out QRCodeData? qRCodeData))
                return await GetReceiptFromQRData(qRCodeData);

            TryGetReceiptsResultUnit result = new TryGetReceiptsResultUnit();
            result.Fail = new FailData();
            result.Fail.FileName = ftsData;
            result.Fail.Code = FailData.ErrorCode.IncorrectQRData;

            return result;
        }

        public async Task<TryGetReceiptsResultUnit> GetReceiptFromQRData(QRCodeData qRCodeData)
        {
            TryGetReceiptsResultUnit result = new TryGetReceiptsResultUnit();

            bool isDataBaseContainReceipt = false;

            FTSResponseResult fTSResponseResult = null;
            FailData.ErrorCode errorCode = FailData.ErrorCode.UnknownError;

            if (((isDataBaseContainReceipt = await database.IsContainReceiptAsync(qRCodeData.FullStringData)) == false) &&
                ((fTSResponseResult = await fts.RequestAsync(qRCodeData)).StatusCode == FTSResponseResult.ServerResponseCode.Success) &&
                ((errorCode = Receipt.ParseReceiptFromJson(fTSResponseResult.Response, qRCodeData.FullStringData, out Receipt? receipt)) == FailData.ErrorCode.Success))
            {
                result.Receipt = receipt;
            }
            else
            {
                result.Fail = new FailData();
                result.Fail.FileName = qRCodeData.FullStringData;
                result.Fail.ServerResponseCode = (fTSResponseResult == null ? -1 : fTSResponseResult.ResponseCode);

                if (isDataBaseContainReceipt)
                {
                    result.Fail.Code = FailData.ErrorCode.AlreadyExistsInDatabase;
                }
                else if (fTSResponseResult.StatusCode != FTSResponseResult.ServerResponseCode.Success)
                {
                    result.Fail.Code = FailData.RecognizeServerStatus(fTSResponseResult.StatusCode);
                }
                else result.Fail.Code = errorCode;
            }

            return result;
        }

        public void AddUserReceipt(Receipt receipt)
        {
            database.AddNewReceipts(new List<Receipt>() { receipt });
        }

        public bool TryGetProductFromDatabaseById(int id, out Product? product)
        {
            return database.TryGetProductById(id, out product);
        }


        public bool CheckAndGetUserReceipt(int id, out Receipt? userReceipt)
        {
            if (database.TryGetReceiptById(id, out userReceipt) && Database.IsUsersReceipt(userReceipt))
            {
                return true;
            }

            userReceipt = null;
            return false;
        }

        public async void AddReceiptsInDatabase(List<Receipt> receipts)
        {
            database.AddNewReceipts(receipts);
        }

        public void ChangeProductCategory(Product product, ProductCategory newCategory)
        {
            product.Category = newCategory;
            database.ChangeProduct(product.Id, product);
        }

        public void ChangeUserReceipt(Receipt receipt)
        {
            if (Database.IsUsersReceipt(receipt))
            {
                database.ChangeReceipt(receipt.Id, receipt);
            }
        }

        public void DeleteReceipt(int id)
        {
            database.DeleteReceipt(id);
        }

        public IEnumerable<Receipt> GetReceiptsFromDB()
        {
            foreach (Receipt receipt in database.GetAllReceiptsWithCurrentConditionString())
                yield return receipt;
        }

        public IEnumerable<Product> GetProductsFromDB()
        {
            foreach (Product product in database.GetAllProductsWithCurrentConditionString())
                yield return product;
        }

        public IEnumerable<StatisticDataUnit> GetReceiptsDuringPeriod(DateTime from, DateTime until, (int day, int month, int year) interval)
        {
            until = until.Add(new TimeSpan(23, 59, 59));

            DateTime fromLocal = from;

            DateTime untilLocal = from;
            untilLocal = untilLocal.AddDays(interval.Item1);
            untilLocal = untilLocal.AddMonths(interval.Item2);
            untilLocal = untilLocal.AddYears(interval.Item3);

            bool continueFlag = true;

            for (; continueFlag;)
            {
                StatisticDataUnit data = new StatisticDataUnit();

                if (untilLocal >= until)
                {
                    untilLocal = until;
                    continueFlag = false;
                }

                foreach (Receipt receipt in database.GetReceiptsDuringPeriod(fromLocal, untilLocal))
                {
                    data.Value += Double.Round(receipt.TotalSum, 2);
                    data.Value = Double.Round(data.Value, 2);
                }

                data.date = fromLocal;

                if (continueFlag)
                {
                    fromLocal = untilLocal;

                    untilLocal = untilLocal.AddDays(interval.Item1);
                    untilLocal = untilLocal.AddMonths(interval.Item2);
                    untilLocal = untilLocal.AddYears(interval.Item3);
                }

                yield return data;
            }
        }

        public IEnumerable<StatisticDataUnit> GetCurrentMonthReceipts()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            int countDays = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).Day;

            for (int day = 1; day < countDays + 1; day++)
            {
                StatisticDataUnit data = new StatisticDataUnit();

                DateTime from = new DateTime(year, month, day);
                DateTime until = from.Add(new TimeSpan(23, 59, 59));

                foreach (Receipt receipt in database.GetReceiptsDuringPeriod(from, until))
                {
                    data.Value += Double.Round(receipt.TotalSum, 2);
                    data.Value = Double.Round(data.Value, 2);
                }

                data.date = from;
                yield return data;
            }
        }

        public IEnumerable<StatisticDataUnit> GetCurrentYearReceipts()
        {
            int year = DateTime.Now.Year;

            for (int month = 1; month < 13; month++)
            {
                StatisticDataUnit data = new StatisticDataUnit();

                DateTime from = new DateTime(year, month, 1);
                DateTime until = from.AddMonths(1).AddDays(-1);
                until = until.Add(new TimeSpan(23, 59, 59));

                foreach (Receipt receipt in database.GetReceiptsDuringPeriod(from, until))
                {
                    data.Value += Double.Round(receipt.TotalSum, 2);
                    data.Value = Double.Round(data.Value, 2);
                }

                data.date = from;
                yield return data;
            }
        }

        public double GetTotalSumDuringPeriod(int fromYear, int fromMonth, int untilYear, int untilMonth)
        {
            double totalSum = 0;

            DateTime from = new DateTime(fromYear, fromMonth, 1);
            DateTime until = new DateTime(untilYear, untilMonth, 1).AddMonths(1).AddDays(-1).Add(new TimeSpan(23, 59, 59));

            foreach (Receipt receipt in database.GetReceiptsDuringPeriod(from, until))
                totalSum += Double.Round(receipt.TotalSum, 2);

            totalSum = Double.Round(totalSum, 2);

            return totalSum;
        }

        public Dictionary<string, double> GetProductCategoryStatisticDuringYear(int year)
        {
            Dictionary<string, double> productCategoryStatistic = new Dictionary<string, double>();

            DateTime from = new DateTime(year, 1, 1);
            DateTime until = new DateTime(year, 12, 31).Add(new TimeSpan(23, 59, 59));

            foreach (Receipt receipt in database.GetReceiptsDuringPeriod(from, until))
            {
                foreach (Product product in receipt.ListOfProducts)
                {
                    if (productCategoryStatistic.ContainsKey(product.Category.Name))
                    {
                        productCategoryStatistic[product.Category.Name] += product.Sum;
                        productCategoryStatistic[product.Category.Name] = Double.Round(productCategoryStatistic[product.Category.Name], 2);
                    }
                    else
                    {
                        productCategoryStatistic.Add(product.Category.Name, product.Sum);
                    }
                }
            }

            productCategoryStatistic = productCategoryStatistic.OrderByDescending<KeyValuePair<string, double>, double>((pair) => pair.Value).ToDictionary();

            return productCategoryStatistic;
        }
    }
}
