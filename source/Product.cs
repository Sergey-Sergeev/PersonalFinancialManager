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
        public int Count { get; private set; }
        public ProductCategory Category { get; set; }


        public Product(string Name, double Price, int Count, ProductCategory Category)
        {
            this.Name = Name;
            this.Price = Price;
            this.Count = Count;
            this.Category = Category;
        }

    }
}
