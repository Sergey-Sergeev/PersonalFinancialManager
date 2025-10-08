using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManager.source
{
    public class Product
    {
        public string Name { get; private set; }
        public double Price { get; private set; }   
        public double Quantity { get; private set; }
        public double Sum { get; private set; }
        public ProductCategory Category { get; set; }


        public Product(string name, double price, double quantity, double sum, ProductCategory category)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Sum = sum;
            Category = category;
        }

    }
}
