using System.Linq;
using System.Text.Json;
using System.Xml.Linq;
using static PersonalFinancialManager.source.JsonServerClass;

namespace PersonalFinancialManager.source
{
    public class Receipt
    {
        public List<Product> ListOfProducts { get; private set; }
        public double TotalSum { get; private set; }

        public DateTime DateAndTime { get; private set; }

        public double CashTotalSum { get; private set; }
        public double EcashTotalSum { get; private set; }
        public string RetailPlaceAddress { get; private set; }
        public string? FullFtsReceiptData { get; private set; }

        public int Id { get; private set; }

        private Receipt()
        {
            ListOfProducts = new List<Product>();
        }

        public Receipt(List<Product> listOfProducts, double totalPrice, DateTime dateTime, double cashTotalSum, double ecashTotalSum, string retailPlaceAddress, string? fullFtsReceiptData, int id = -1)
        {
            ListOfProducts = listOfProducts;
            TotalSum = totalPrice;
            DateAndTime = dateTime;
            CashTotalSum = cashTotalSum;
            EcashTotalSum = ecashTotalSum;
            RetailPlaceAddress = retailPlaceAddress;
            FullFtsReceiptData = fullFtsReceiptData;
            Id = id;
        }


        public static TryGetReceiptsResultUnit.FailData.ErrorCode ParseReceiptFromJson(string json, string fullFtsReceiptData, out Receipt? receipt)
        {
            TryGetReceiptsResultUnit.FailData.ErrorCode result;
            JsonServerClass? jsonClass = null;
            receipt = null;

            try
            {
                jsonClass = JsonSerializer.Deserialize<JsonServerClass>(json, new JsonSerializerOptions()
                {
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true,
                });
            }
            catch { }


            if (jsonClass == null)
            {
                JsonServerCode? jsServerCode = null;
                try
                {
                    jsServerCode = JsonSerializer.Deserialize<JsonServerCode>(json, new JsonSerializerOptions()
                    {
                        ReadCommentHandling = JsonCommentHandling.Skip,
                        AllowTrailingCommas = true,
                    });
                }
                catch { }

                if (jsServerCode == null)
                {
                    return TryGetReceiptsResultUnit.FailData.ErrorCode.FailDeserializeJSON;
                }
                return TryGetReceiptsResultUnit.FailData.RecognizeServerCodeFromJson(jsServerCode.code);
            }

            result = TryGetReceiptsResultUnit.FailData.RecognizeServerCodeFromJson(jsonClass.code);

            if (result == TryGetReceiptsResultUnit.FailData.ErrorCode.Success)
            {
                if (jsonClass.data.json.items.Count == 0)
                {
                    return TryGetReceiptsResultUnit.FailData.ErrorCode.FailDeserializeJSON;
                }

                receipt = new Receipt();

                if (DateTime.TryParse(jsonClass.data.json.dateTime, out DateTime dateTime))     // "2025-10-07T18:20:00"
                    receipt.DateAndTime = dateTime;
                else return TryGetReceiptsResultUnit.FailData.ErrorCode.FailDeserializeJSON;

                receipt.TotalSum = (Double)(jsonClass.data.json.totalSum) / 100;  // convert to rubs
                receipt.EcashTotalSum = (Double)(jsonClass.data.json.ecashTotalSum) / 100;
                receipt.CashTotalSum = (Double)(jsonClass.data.json.cashTotalSum) / 100;
                receipt.RetailPlaceAddress = ChangeQuotations(jsonClass.data.json.retailPlaceAddress);
                receipt.FullFtsReceiptData = fullFtsReceiptData;

                for (int i = 0; i < jsonClass.data.json.items.Count; i++)
                {
                    Product product = new Product(
                        ChangeQuotations(jsonClass.data.json.items[i].name),
                        (Double)(jsonClass.data.json.items[i].price) / 100,
                        jsonClass.data.json.items[i].quantity,
                        (Double)(jsonClass.data.json.items[i].sum) / 100,
                        new ProductCategory()
                        );
                    receipt.ListOfProducts.Add(product);
                }

            }

            return result;
        }

        private static string ChangeQuotations(string str)
        {
            string result = str.Replace("\"", "'");

            if (result.Count((c) => c == '\'') % 2 != 0)
                result += "'";

            return result;
        }

        private class JsonServerCode
        {
            public int code { get; set; }
            public string data { get; set; }
        }
    }
}
