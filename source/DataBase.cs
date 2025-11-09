using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PersonalFinancialManager.source
{
    public class Database
    {
        private static Database? singleInstance = null;

        private SqliteConnection sqlConnection;
        private SqliteCommand sqlCommand;

        public const int MAX_RECEIPTS_READ_AT_LOAD = 1000;

        public int ReceiptsCountReadAtLoad
        {
            get => receiptsCountReadAtLoad;
            set
            {
                receiptsCountReadAtLoad = value > MAX_RECEIPTS_READ_AT_LOAD ? MAX_RECEIPTS_READ_AT_LOAD : value;
            }
        }
        private int receiptsCountReadAtLoad = 300;


        private const string DATABASE_NAME = "PFMDataBase";
        private const string CONNECTION_STRING = $"Data Source={DATABASE_NAME}.db;";
        private const string RECEIPTS_DATA_TABLE_NAME = "receipts";
        private const string PRODUCTS_DATA_TABLE_NAME = "products";
        private const string USER_DATA_TABLE_NAME = "user";
        private const int DATABASE_FIXED_STRING_LEN = 160;

        private const string DATA_DOESNT_EXIST_MARK = "NULL";


        private class ProductDBNames
        {
            public const string ID = "id";
            public const string RECEIPT_ID = "receiptId";
            public const string NAME = "name";
            public const string PRICE = "price";
            public const string QUANTITY = "quantity";
            public const string SUM = "sum";
            public const string CATEGORY = "category";
        }
        private class ReceiptDBNames
        {
            public const string ID = "id";
            public const string DATE_AND_TIME = "date";
            public const string ADDRESS = "address";
            public const string TOTAL_SUM = "totalSum";
            public const string CASH_SUM = "cashSum";
            public const string E_CASH_SUM = "eCashSum";
            public const string FULL_FTS_RECEIPT_DATA = "fullFtsReceiptData";
        }
        private class UserDBNames
        {
            public const string TOKEN = "token";
        }

        private readonly string CREATE_PRODUCT_TABLE_COMMAND = $"CREATE TABLE IF NOT EXISTS {PRODUCTS_DATA_TABLE_NAME} " +
                $"({ProductDBNames.ID} INTEGER PRIMARY KEY," +
                $" {ProductDBNames.RECEIPT_ID} INTEGER," +
                $" {ProductDBNames.NAME} NVARCHAR({DATABASE_FIXED_STRING_LEN})," +
                $" {ProductDBNames.PRICE} REAL," +
                $" {ProductDBNames.QUANTITY} REAL," +
                $" {ProductDBNames.SUM} REAL," +
                $" {ProductDBNames.CATEGORY} NVARCHAR({DATABASE_FIXED_STRING_LEN}));";

        private readonly string CREATE_RECEIPT_TABLE_COMMAND = $"CREATE TABLE IF NOT EXISTS {RECEIPTS_DATA_TABLE_NAME} " +
                $"({ReceiptDBNames.ID} INTEGER PRIMARY KEY AUTOINCREMENT," +
                $" {ReceiptDBNames.DATE_AND_TIME} DATETIME," +
                $" {ReceiptDBNames.ADDRESS} NVARCHAR({DATABASE_FIXED_STRING_LEN})," +
                $" {ReceiptDBNames.TOTAL_SUM} REAL," +
                $" {ReceiptDBNames.CASH_SUM} REAL," +
                $" {ReceiptDBNames.E_CASH_SUM} REAL," +
                $" {ReceiptDBNames.FULL_FTS_RECEIPT_DATA} NVARCHAR({DATABASE_FIXED_STRING_LEN}));";


        private readonly string CREATE_USER_TABLE_COMMAND = $"CREATE TABLE IF NOT EXISTS {USER_DATA_TABLE_NAME} " +
                $"({UserDBNames.TOKEN} NVARCHAR({DATABASE_FIXED_STRING_LEN}));";



        private Database()
        {
            sqlConnection = new SqliteConnection(CONNECTION_STRING);
            sqlConnection.Open();
            sqlCommand = new SqliteCommand();
            sqlCommand.Connection = sqlConnection;

            CreateUserTable();
            SendCommand(CREATE_RECEIPT_TABLE_COMMAND);
            SendCommand(CREATE_PRODUCT_TABLE_COMMAND);
        }

        ~Database()
        {
            sqlCommand.Dispose();
            sqlConnection.Close();
        }


        public static bool IsUsersReceipt(Receipt receipt)
        {
            return (receipt.FullFtsReceiptData == DATA_DOESNT_EXIST_MARK) || (receipt.FullFtsReceiptData == null);
        }

        public static Database Fabric()
        {
            if (singleInstance != null)
                return singleInstance;

            singleInstance = new Database();
            return singleInstance;
        }

        public bool IsUserAuthorizated(out string? userToken)
        {
            return TryGetUserToken(out userToken);
        }

        public void AddNewReceipts(List<Receipt> receipts)
        {
            int receiptId = GetLastReceiptId() + 1;

            for (int i = 0; i < receipts.Count; i++)
            {
                if (AddReceipt(receipts[i], receiptId) != 0)
                {
                    for (int k = 0; k < receipts[i].ListOfProducts.Count; k++)
                    {
                        AddProduct(receiptId, receipts[i].ListOfProducts[k]);
                    }

                    receiptId++;
                }
            }
        }

        public List<string> GetAllUniqueProductCategories()
        {
            List<string> categories = new List<string>();

            sqlCommand.CommandText = $"SELECT {ProductDBNames.CATEGORY} FROM {PRODUCTS_DATA_TABLE_NAME};";
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                string category = (string)reader[ProductDBNames.CATEGORY];

                if (!categories.Contains(category))
                {
                    categories.Add(category);
                }
            }

            reader.Close();

            return categories;
        }

        public IEnumerable<string> GetAllUniqueAddresses()
        {
            List<string> addresses = new List<string>();

            sqlCommand.CommandText = $"SELECT {ReceiptDBNames.ADDRESS} FROM {RECEIPTS_DATA_TABLE_NAME};";
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                string address = (string)reader[ReceiptDBNames.ADDRESS];

                if (!addresses.Contains(address))
                {
                    addresses.Add(address);
                    yield return address;
                }
            }

            reader.Close();
        }

        public bool TryGetProductById(int id, out Product? product)
        {
            sqlCommand.CommandText = $"SELECT * FROM {PRODUCTS_DATA_TABLE_NAME} WHERE {ProductDBNames.ID} = {id};";
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            bool result = false;
            product = null;

            if (reader.Read())
            {
                product = ParseProductFromDatabaseReader(reader);
                result = true;
            }

            reader.Close();

            return result;
        }

        public bool TryGetReceiptById(int id, out Receipt? receipt)
        {
            sqlCommand.CommandText = $"SELECT * FROM {RECEIPTS_DATA_TABLE_NAME} WHERE {ReceiptDBNames.ID} = {id};";
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            bool result = false;
            receipt = null;

            if (reader.Read())
            {
                receipt = ParseReceiptFromDatabaseReader(reader);
                result = true;
            }

            reader.Close();

            return result;
        }

        private Receipt ParseReceiptFromDatabaseReader(SqliteDataReader reader)
        {
            return new Receipt(
                GetReceiptProducts(Convert.ToInt32(reader[ReceiptDBNames.ID])),
                Convert.ToDouble(reader[ReceiptDBNames.TOTAL_SUM]),
                DateTime.Parse((string)reader[ReceiptDBNames.DATE_AND_TIME]),
                Convert.ToDouble(reader[ReceiptDBNames.CASH_SUM]),
                Convert.ToDouble(reader[ReceiptDBNames.E_CASH_SUM]),
                (string)reader[ReceiptDBNames.ADDRESS],
                (string)reader[ReceiptDBNames.FULL_FTS_RECEIPT_DATA],
                Convert.ToInt32(reader[ReceiptDBNames.ID])
                );
        }

        public IEnumerable<Receipt> GetAllReceipts()
        {
            sqlCommand.CommandText = $"SELECT * FROM {RECEIPTS_DATA_TABLE_NAME};";
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            int count = 0;

            while (reader.Read() && count < ReceiptsCountReadAtLoad)
            {
                count++;
                yield return ParseReceiptFromDatabaseReader(reader);
            }

            reader.Close();
        }

        public IEnumerable<Receipt> GetReceiptsDuringPeriod(DateTime from, DateTime until)
        {
            sqlCommand.CommandText = $"SELECT * FROM {RECEIPTS_DATA_TABLE_NAME}" +
                $" WHERE {ReceiptDBNames.DATE_AND_TIME}" +
                $" BETWEEN '{ConvertDateTimeToSqlFormat(from)}' AND '{ConvertDateTimeToSqlFormat(until)}';";

            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                yield return ParseReceiptFromDatabaseReader(reader);
            }

            reader.Close();
        }

        public void ChangeReceipt(int id, Receipt receipt)
        {
            DeleteReceipt(id);
            AddNewReceipts(new List<Receipt>() { receipt });
        }

        public void ChangeProduct(int id, Product product)
        {
            SendCommand($"UPDATE {PRODUCTS_DATA_TABLE_NAME} SET" +
                $" {ProductDBNames.CATEGORY} = '{product.Category.Name}'" +
                $" WHERE {ProductDBNames.ID} = {id};");
        }

        public void DeleteReceipt(int id)
        {
            if (SendCommand($"DELETE FROM {RECEIPTS_DATA_TABLE_NAME} WHERE {ReceiptDBNames.ID} = {id};") != 0)
                SendCommand($"DELETE FROM {PRODUCTS_DATA_TABLE_NAME} WHERE {ProductDBNames.RECEIPT_ID} = {id};");
        }

        public async Task<bool> IsContainReceiptAsync(string fullFtsReceiptData)
        {
            sqlCommand.CommandText = $"SELECT * FROM {RECEIPTS_DATA_TABLE_NAME} WHERE {ReceiptDBNames.FULL_FTS_RECEIPT_DATA} = '{fullFtsReceiptData}';";
            SqliteDataReader reader = await sqlCommand.ExecuteReaderAsync();
            bool res = reader.Read();
            reader.Close();
            return res;
        }

        public void SetUserData(string token)
        {
            SendCommand($"UPDATE {USER_DATA_TABLE_NAME} SET {UserDBNames.TOKEN} = '{token}';");
        }

        private List<Product> GetReceiptProducts(int receiptId)
        {
            List<Product> list = new List<Product>();

            SqliteConnection connection = new SqliteConnection(CONNECTION_STRING);
            connection.Open();
            SqliteDataReader reader = new SqliteCommand($"SELECT * FROM {PRODUCTS_DATA_TABLE_NAME} WHERE {ProductDBNames.RECEIPT_ID} = {receiptId};", connection).ExecuteReader();

            while (reader.Read())
            {
                list.Add(ParseProductFromDatabaseReader(reader));
            }

            reader.Close();
            return list;
        }

        private static Product ParseProductFromDatabaseReader(SqliteDataReader reader)
        {
            return new Product((string)reader[ProductDBNames.NAME],
                                Convert.ToDouble(reader[ProductDBNames.PRICE]),
                                Convert.ToDouble(reader[ProductDBNames.QUANTITY]),
                                Convert.ToDouble(reader[ProductDBNames.SUM]),
                                new ProductCategory((string)reader[ProductDBNames.CATEGORY]),
                                Convert.ToInt32(reader[ProductDBNames.ID]));
        }

        private void CreateUserTable()
        {
            SendCommand(CREATE_USER_TABLE_COMMAND);

            if (!TryGetUserToken(out string? token))
            {
                SendCommand($"INSERT INTO {USER_DATA_TABLE_NAME} " +
                        $"({UserDBNames.TOKEN}) " +
                        $"VALUES " +
                        $"(NULL);");
            }
        }

        private string ConvertDateTimeToSqlFormat(DateTime dateTime)
        {
            // DATETIME - format: YYYY-MM-DD HH:MI:SS
            return $"{dateTime.ToString("yyyy")}-{dateTime.ToString("MM")}-{dateTime.ToString("dd")} {dateTime.ToString("HH")}:{dateTime.ToString("mm")}:{dateTime.ToString("ss")}";
        }

        private int SendCommand(string cmd)
        {
            sqlCommand.CommandText = cmd;
            return sqlCommand.ExecuteNonQuery();
        }

        private int GetLastReceiptId()
        {
            sqlCommand.CommandText = $"SELECT * FROM {RECEIPTS_DATA_TABLE_NAME} ORDER BY ROWID DESC LIMIT 1;";
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            if (!reader.Read())
            {
                reader.Close();
                return -1;
            }

            int res = Convert.ToInt32(reader[ReceiptDBNames.ID]);
            reader.Close();
            return res;
        }

        private int AddReceipt(Receipt receipt, int id)
        {
            string? fullftsData = DATA_DOESNT_EXIST_MARK;

            if (receipt.FullFtsReceiptData != null)
                fullftsData = ConvertStringLenToDatabaseFixedStringLen(receipt.FullFtsReceiptData);


            return SendCommand($"INSERT OR IGNORE INTO {RECEIPTS_DATA_TABLE_NAME} " +
                $"({ReceiptDBNames.ID}," +
                $" {ReceiptDBNames.DATE_AND_TIME}," +
                $" {ReceiptDBNames.ADDRESS}," +
                $" {ReceiptDBNames.TOTAL_SUM}," +
                $" {ReceiptDBNames.CASH_SUM}," +
                $" {ReceiptDBNames.E_CASH_SUM}," +
                $" {ReceiptDBNames.FULL_FTS_RECEIPT_DATA}) " +
                $"VALUES " +
                $"(\"{id}\"," +
                $" \"{ConvertDateTimeToSqlFormat(receipt.DateAndTime)}\"," +
                $" \"{ConvertStringLenToDatabaseFixedStringLen(receipt.RetailPlaceAddress)}\"," +
                $" \"{receipt.TotalSum}\"," +
                $" \"{receipt.CashTotalSum}\"," +
                $" \"{receipt.EcashTotalSum}\"," +
                $" \"{fullftsData}\");");
        }

        private int AddProduct(int receiptId, Product product)
        {
            return SendCommand($"INSERT OR IGNORE INTO {PRODUCTS_DATA_TABLE_NAME} " +
                $"({ProductDBNames.RECEIPT_ID}," +
                $" {ProductDBNames.NAME}," +
                $" {ProductDBNames.PRICE}," +
                $" {ProductDBNames.QUANTITY}," +
                $" {ProductDBNames.SUM}," +
                $" {ProductDBNames.CATEGORY}) " +
                $"VALUES " +
                $"(\"{receiptId}\"," +
                $" \"{ConvertStringLenToDatabaseFixedStringLen(product.Name)}\"," +
                $" \"{product.Price}\"," +
                $" \"{product.Quantity}\"," +
                $" \"{product.Sum}\"," +
                $" \"{ConvertStringLenToDatabaseFixedStringLen(product.Category.Name)}\");");
        }

        private bool TryGetUserToken(out string? token)
        {
            token = null;

            sqlCommand.CommandText = $"SELECT * FROM {USER_DATA_TABLE_NAME} WHERE {UserDBNames.TOKEN} IS NOT NULL;";
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            if (reader.Read())
                token = (string)(reader[UserDBNames.TOKEN]);

            reader.Close();

            return token != null;
        }

        private string ConvertStringLenToDatabaseFixedStringLen(string str)
        {
            if (str.Length < DATABASE_FIXED_STRING_LEN)
            {
                return str;
            }
            else
            {
                int start = str.Length - DATABASE_FIXED_STRING_LEN;
                return str.Substring(start, DATABASE_FIXED_STRING_LEN);
            }
        }

    }
}
