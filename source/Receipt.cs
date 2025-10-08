using System.Text.Json;
using System.Xml.Linq;

namespace PersonalFinancialManager.source
{
    public class Receipt
    {
        public List<Product> ListOfProducts { get; private set; }
        public double TotalPrice { get; private set; }

        public string DateAndTime { get; private set; }
        public double CashTotalSum { get; private set; }
        public double EcashTotalSum { get; private set; }
        public string RetailPlaceAddress { get; private set; }

        private Receipt() 
        {
            ListOfProducts = new List<Product>();
        }


        public static FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode ParseReceiptFromJson(string json, out Receipt? receipt)
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

            if(jsonClass == null)
                return FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode.FailDeserializeJSON;


            result = FTSDecodingReceiptsResult.FailGettingReceiptData.RecognizeServerCodeFromJson(jsonClass.code);

            if (result == FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode.Success)
            {
                receipt = new Receipt();

                receipt.DateAndTime = jsonClass.data.json.dateTime;
                receipt.TotalPrice = (Double)(jsonClass.data.json.totalSum) / 100;  // convert to rubs
                receipt.EcashTotalSum = (Double)(jsonClass.data.json.ecashTotalSum) / 100;
                receipt.CashTotalSum = (Double)(jsonClass.data.json.cashTotalSum) / 100;
                receipt.RetailPlaceAddress = jsonClass.data.json.retailPlaceAddress;


                for (int i = 0; i < jsonClass.data.json.items.Count; i++)
                {
                    Product product = new Product(
                        jsonClass.data.json.items[i].name,
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
    }
}
