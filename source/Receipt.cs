using System.Text.Json;
using System.Xml.Linq;
using static PersonalFinancialManager.source.JsonServerClass;

namespace PersonalFinancialManager.source
{
    public class Receipt
    {
        public List<Product> ListOfProducts { get; private set; }
        public double TotalPrice { get; private set; }

        public string DateAndTimeString { get => dateAndTimeString;
            private set {
                DateAndTime = DateTime.Parse(value);
                dateAndTimeString = value;
            }
        }
        private string dateAndTimeString;


        public DateTime DateAndTime { get; private set; }

        public double CashTotalSum { get; private set; }
        public double EcashTotalSum { get; private set; }
        public string RetailPlaceAddress { get; private set; }
        public string FullFtsReceiptData { get; private set; }

        private Receipt()
        {
            ListOfProducts = new List<Product>();
        }

        public Receipt(List<Product> listOfProducts, double totalPrice, string dateAndTimeString, double cashTotalSum, double ecashTotalSum, string retailPlaceAddress, string fullFtsReceiptData)
        {
            ListOfProducts = listOfProducts;
            TotalPrice = totalPrice;
            DateAndTimeString = dateAndTimeString;
            CashTotalSum = cashTotalSum;
            EcashTotalSum = ecashTotalSum;
            RetailPlaceAddress = retailPlaceAddress;
            FullFtsReceiptData = fullFtsReceiptData;
        }


        public static FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode ParseReceiptFromJson(string json, string fullFtsReceiptData, out Receipt? receipt)
        {
            FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode result;
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
                    return FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode.FailDeserializeJSON;
                }
                return FTSDecodingReceiptsResult.FailGettingReceiptData.RecognizeServerCodeFromJson(jsServerCode.code);
            }

            result = FTSDecodingReceiptsResult.FailGettingReceiptData.RecognizeServerCodeFromJson(jsonClass.code);

            if (result == FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode.Success)
            {
                if (jsonClass.data.json.items.Count == 0)
                {
                    return FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode.FailDeserializeJSON;
                }

                receipt = new Receipt();

                receipt.DateAndTimeString = jsonClass.data.json.dateTime;     // "2025-10-07T18:20:00"
                receipt.TotalPrice = (Double)(jsonClass.data.json.totalSum) / 100;  // convert to rubs
                receipt.EcashTotalSum = (Double)(jsonClass.data.json.ecashTotalSum) / 100;
                receipt.CashTotalSum = (Double)(jsonClass.data.json.cashTotalSum) / 100;
                receipt.RetailPlaceAddress = jsonClass.data.json.retailPlaceAddress.Replace("\"", "'");
                receipt.FullFtsReceiptData = fullFtsReceiptData;

                for (int i = 0; i < jsonClass.data.json.items.Count; i++)
                {
                    Product product = new Product(
                        jsonClass.data.json.items[i].name.Replace("\"", "'"),
                        (Double)(jsonClass.data.json.items[i].price) / 100,
                        jsonClass.data.json.items[i].quantity,
                        (Double)(jsonClass.data.json.items[i].sum) / 100,
                        ProductCategory.AutoSetProductCategory(jsonClass.data.json.items[i].name)
                        );
                    receipt.ListOfProducts.Add(product);
                }

            }

            return result;
        }

        private class JsonServerCode
        {
            public int code { get; set; }
            public string data { get; set; }
        }
    }
}
