using System.Xml.Linq;

namespace PersonalFinancialManager.source
{
    public class Receipt
    {
        public List<Product> listOfProducts;
        public double TotalPrice { get; private set; }

        public QRCodeData QRData { get; private set; }

        public Receipt(QRCodeData qRData, List<Product> products) 
        {
            QRData = qRData;
            TotalPrice = Double.Parse(QRData.S) / 100;
            listOfProducts = products;
        }

        public static List<Product> ParseProductsFromJSON(string json)
        {
            List<Product> list = new List<Product>();


            //    TO DO: Parser


            return list;
        }
    }
}
