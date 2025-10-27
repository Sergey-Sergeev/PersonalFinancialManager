﻿using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Net;

namespace PersonalFinancialManager.source
{
    public class DataBase
    {
        public string? UserToken { get; private set; } = null;

        public const int MAX_RECEIPTS_READ_AT_LOAD = 1000;

        public int ReceiptsCountReadAtLoad { 
            get => receiptsCountReadAtLoad; 
            set { receiptsCountReadAtLoad = value > MAX_RECEIPTS_READ_AT_LOAD ? MAX_RECEIPTS_READ_AT_LOAD : value; 
            } }
        private int receiptsCountReadAtLoad = 300;


        private const string DATABASE_NAME = "PFMDataBase";
        private const string CONNECTION_STRING = $"Data Source={DATABASE_NAME}.db;";
        private const string RECEIPTS_DATA_TABLE_NAME = "receipts";
        private const string PRODUCTS_DATA_TABLE_NAME = "products";
        private const string USER_DATA_TABLE_NAME = "user";
        private const int DATABASE_FIXED_STRING_LEN = 160;


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

        private readonly string CREATE_PRODUCT_TABLE_COMMAND = $"CREATE TABLE IF NOT EXISTS {PRODUCTS_DATA_TABLE_NAME} " +
                $"({ProductDBNames.ID} INTEGER PRIMARY KEY AUTOINCREMENT," +
                $" {ProductDBNames.RECEIPT_ID} INTEGER," +
                $" {ProductDBNames.NAME} NVARCHAR({DATABASE_FIXED_STRING_LEN})," +
                $" {ProductDBNames.PRICE} REAL," +
                $" {ProductDBNames.QUANTITY} REAL," +
                $" {ProductDBNames.SUM} REAL," +
                $" {ProductDBNames.CATEGORY} NVARCHAR({DATABASE_FIXED_STRING_LEN}));";

        private class ReceiptDBNames
        {
            public const string ID = "id";
            public const string DATE_AND_TIME = "DateTime";
            public const string ADDRESS = "address";
            public const string TOTAL_SUM = "totalSum";
            public const string CASH_SUM = "cashSum";
            public const string E_CASH_SUM = "eCashSum";
            public const string FULL_FTS_RECEIPT_DATA = "fullFtsReceiptData";
        }

        private readonly string CREATE_RECEIPT_TABLE_COMMAND = $"CREATE TABLE IF NOT EXISTS {RECEIPTS_DATA_TABLE_NAME} " +
                $"({ReceiptDBNames.ID} INTEGER PRIMARY KEY AUTOINCREMENT," +
                $" {ReceiptDBNames.DATE_AND_TIME} DATETIME UNIQUE," +
                $" {ReceiptDBNames.ADDRESS} NVARCHAR({DATABASE_FIXED_STRING_LEN})," +
                $" {ReceiptDBNames.TOTAL_SUM} REAL," +
                $" {ReceiptDBNames.CASH_SUM} REAL," +
                $" {ReceiptDBNames.E_CASH_SUM} REAL," +
                $" {ReceiptDBNames.FULL_FTS_RECEIPT_DATA} NVARCHAR({DATABASE_FIXED_STRING_LEN}) UNIQUE);";

        private class UserDBNames
        {
            public const string TOKEN = "token";
            public const string RECEIPTS_SCANNING_COUNT = "receiptsCount";
        }

        private readonly string CREATE_USER_TABLE_COMMAND = $"CREATE TABLE IF NOT EXISTS {USER_DATA_TABLE_NAME} " +
                $"({UserDBNames.TOKEN} NVARCHAR({DATABASE_FIXED_STRING_LEN})," +
                $" {UserDBNames.RECEIPTS_SCANNING_COUNT} INTEGER);";

        private static DataBase? dataBase = null;

        private SqliteConnection sqlConnection;
        private SqliteCommand sqlCommand;

        private DataBase()
        {
            sqlConnection = new SqliteConnection(CONNECTION_STRING);
            sqlConnection.Open();
            sqlCommand = new SqliteCommand();
            sqlCommand.Connection = sqlConnection;

            SendCommand(CREATE_USER_TABLE_COMMAND);
            SendCommand(CREATE_RECEIPT_TABLE_COMMAND);
            SendCommand(CREATE_PRODUCT_TABLE_COMMAND);
        }

        ~DataBase()
        {
            sqlCommand.Dispose();
            sqlConnection.Close();
        }


        public static DataBase Fabric()
        {
            if (dataBase != null)
                return dataBase;

            dataBase = new DataBase();
            return dataBase;
        }

        public bool IsUserAuthorizated()
        {
            return (UserToken = TryGetUserToken()) != null;
        }

        public void AddNewReceipts(List<Receipt> receipts)
        {
            int lastReceiptId = GetLastReceiptId();

            for (int i = 0; i < receipts.Count; i++)
            {
                if (AddReceipt(receipts[i]) != 0)
                {
                    lastReceiptId++;
                    for (int k = 0; k < receipts[i].ListOfProducts.Count; k++)
                    {
                        AddProduct(lastReceiptId, receipts[i].ListOfProducts[k]);
                    }
                }
            }
        }
        
        
        public IEnumerable<Receipt> GetAllReceipts()
        {
            sqlCommand.CommandText = $"SELECT * FROM {RECEIPTS_DATA_TABLE_NAME};";
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            int count = 0;

            while (reader.Read() && count < ReceiptsCountReadAtLoad)
            {
                count++;

                yield return new Receipt(
                    GetReceiptProducts(Convert.ToInt32(reader[ReceiptDBNames.ID])),
                    Convert.ToDouble(reader[ReceiptDBNames.TOTAL_SUM]),
                    (string)reader[ReceiptDBNames.DATE_AND_TIME],
                    Convert.ToDouble(reader[ReceiptDBNames.CASH_SUM]),
                    Convert.ToDouble(reader[ReceiptDBNames.E_CASH_SUM]),
                    (string)reader[ReceiptDBNames.ADDRESS],
                    (string)reader[ReceiptDBNames.FULL_FTS_RECEIPT_DATA]
                    );
            }

            reader.Close();
        }

        public bool IsContainReceipt(string fullFtsReceiptData)
        {
            sqlCommand.CommandText = $"SELECT * FROM {RECEIPTS_DATA_TABLE_NAME} WHERE {ReceiptDBNames.FULL_FTS_RECEIPT_DATA} = '{fullFtsReceiptData}';";
            SqliteDataReader reader = sqlCommand.ExecuteReader();
            bool res = reader.Read();
            reader.Close();
            return res;
        }
        
        public List<Product> GetReceiptProducts(int receiptId)
        {
            List<Product> list = new List<Product>();

            SqliteConnection connection = new SqliteConnection(CONNECTION_STRING);
            connection.Open();
            SqliteDataReader reader = new SqliteCommand($"SELECT * FROM {PRODUCTS_DATA_TABLE_NAME} WHERE {ProductDBNames.RECEIPT_ID} = {receiptId};", connection).ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Product((string)reader[ProductDBNames.NAME],
                    Convert.ToDouble(reader[ProductDBNames.PRICE]),
                    Convert.ToDouble(reader[ProductDBNames.QUANTITY]),
                    Convert.ToDouble(reader[ProductDBNames.SUM]),
                    new ProductCategory((string)reader[ProductDBNames.CATEGORY])));
            }

            reader.Close();
            return list;
        }

        public int SetUserData(string token)
        {
            int res;
            UserToken = token;

            if (TryGetUserToken() == null)
            {
                res = SendCommand($"INSERT INTO {USER_DATA_TABLE_NAME} " +
                    $"({UserDBNames.TOKEN}," +
                    $" {UserDBNames.RECEIPTS_SCANNING_COUNT}) " +
                    $"VALUES " +
                    $"(\"{token}\"," +
                    $" \"{0}\");");
            }
            else
            {
                res = SendCommand($"UPDATE {USER_DATA_TABLE_NAME} SET {UserDBNames.TOKEN} = '{token}';");
            }
            return res;
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
                return 0;
            }

            int res = Convert.ToInt32(reader[ReceiptDBNames.ID]);
            reader.Close();
            return res;
        }

        private int AddReceipt(Receipt receipt)
        {
            return SendCommand($"INSERT OR IGNORE INTO {RECEIPTS_DATA_TABLE_NAME} " +
                $"({ReceiptDBNames.DATE_AND_TIME}," +
                $" {ReceiptDBNames.ADDRESS}," +
                $" {ReceiptDBNames.TOTAL_SUM}," +
                $" {ReceiptDBNames.CASH_SUM}," +
                $" {ReceiptDBNames.E_CASH_SUM}," +
                $" {ReceiptDBNames.FULL_FTS_RECEIPT_DATA}) " +
                $"VALUES " +
                $"(\"{receipt.DateAndTimeString}\"," +
                $" \"{ConvertStringLenToDatabaseFixedStringLen(receipt.RetailPlaceAddress)}\"," +
                $" \"{receipt.TotalPrice}\"," +
                $" \"{receipt.CashTotalSum}\"," +
                $" \"{receipt.EcashTotalSum}\"," +
                $" \"{ConvertStringLenToDatabaseFixedStringLen(receipt.FullFtsReceiptData)}\");");
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
                $" \"{ConvertStringLenToDatabaseFixedStringLen(product.Category.CategoryName)}\");");
        }

        private string? TryGetUserToken()
        {
            string? res = null;

            sqlCommand.CommandText = $"SELECT * FROM {USER_DATA_TABLE_NAME};";
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                res = (string)(reader[UserDBNames.TOKEN]);
            }

            reader.Close();
            return res;
        }

        private string ConvertStringLenToDatabaseFixedStringLen(string str)
        {
            if (str.Length <= DATABASE_FIXED_STRING_LEN)
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
