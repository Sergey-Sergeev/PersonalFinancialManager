using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PersonalFinancialManager.source.FTSDecodingReceiptsResult;
using static PersonalFinancialManager.source.JsonServerClass;

namespace PersonalFinancialManager.source
{
    public class DataService
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

            isUserAuthorizated = database.IsUserAuthorizated(out string? userToken);
            fts = FTS.Fabric(userToken);

            return singleInstance;
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
        public async Task<FTSDecodingReceiptsResult> GetReceiptsFromQRCodes(string[] files)
        {
            FTSDecodingReceiptsResult fTSResult = new FTSDecodingReceiptsResult();

            bool isDataBaseContainReceipt = false;

            QRCodeData? qRCodeData;
            FTSResponseResult fTSResponseResult = null;
            FailGettingReceiptData.ErrorCode errorCode = FailGettingReceiptData.ErrorCode.UnknownError;

            for (int i = 0; i < files.Length; i++)
            {
                if (((qRCodeData = await QRCodeData.ParseDataFromQRImageAsync(files[i])) != null) &&
                    ((isDataBaseContainReceipt = await database.IsContainReceiptAsync(qRCodeData.FullStringData)) == false) &&
                    ((fTSResponseResult = await fts.RequestAsync(qRCodeData)).StatusCode == FTSResponseResult.ServerResponseCode.Success) &&
                    ((errorCode = Receipt.ParseReceiptFromJson(fTSResponseResult.Response, qRCodeData.FullStringData, out Receipt? receipt)) == FailGettingReceiptData.ErrorCode.Success))
                {
                    fTSResult.Receipts.Add(receipt);
                }
                else
                {
                    FailGettingReceiptData failGettingReceiptData = new FailGettingReceiptData();
                    failGettingReceiptData.FileName = files[i];
                    failGettingReceiptData.ServerResponseCode = (fTSResponseResult == null ? -1 : fTSResponseResult.ResponseCode);

                    if (qRCodeData == null)
                        failGettingReceiptData.Code = FailGettingReceiptData.ErrorCode.DecodingQRFail;
                    else if (isDataBaseContainReceipt)
                    {
                        failGettingReceiptData.Code = FailGettingReceiptData.ErrorCode.AlreadyExistsInDatabase;
                    }
                    else if (fTSResponseResult.StatusCode != FTSResponseResult.ServerResponseCode.Success)
                    {
                        failGettingReceiptData.Code = FailGettingReceiptData.RecognizeServerStatus(fTSResponseResult.StatusCode);

                        if (fTSResponseResult.StatusCode == FTSResponseResult.ServerResponseCode.IncorrectAPIKey)
                        {
                            fTSResult.FailDecoding.Add(failGettingReceiptData);
                            return fTSResult;
                        }
                    }
                    else failGettingReceiptData.Code = errorCode;

                    fTSResult.FailDecoding.Add(failGettingReceiptData);
                }
            }

            if (fTSResult.Receipts.Count != 0)
                database.AddNewReceipts(fTSResult.Receipts);

            return fTSResult;
        }

        public void DeleteReceipt(DateTime time)
        {
            database.DeleteReceipt(time);
        }

        public IEnumerable<Receipt> GetReceiptsFromDB()
        {
            foreach (Receipt receipt in database.GetAllReceipts())
                yield return receipt;
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
                    data.Value += Double.Round(receipt.TotalPrice, 2);
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
                    data.Value += Double.Round(receipt.TotalPrice, 2);
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
                    data.Value += Double.Round(receipt.TotalPrice, 2);
                    data.Value = Double.Round(data.Value, 2);
                }

                data.date = from;
                yield return data;
            }
        }

    }
}
