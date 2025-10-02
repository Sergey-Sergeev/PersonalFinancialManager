using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManager.source
{
    public class Receipt
    {
        public List<Product> listOfProducts;
        public double TotalPrice { get; private set; }



        public Receipt(List<Product> products)
        {
            listOfProducts = products;
            SetTotalPrice();
        }

        private void SetTotalPrice()
        {
            TotalPrice = 0;

            for (int i = 0; i < listOfProducts.Count; i++)
            {
                TotalPrice += listOfProducts[i].Price * listOfProducts[i].Count;
            }
        }


    }
}
