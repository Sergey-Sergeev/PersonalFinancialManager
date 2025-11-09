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

        public int Id { get; private set; }

        public Product(string name, double price, double quantity, double sum, ProductCategory category, int id = -1)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Sum = sum;
            Category = category;
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Product other = obj as Product;
            if (other == null) return false;
            return Equals(other);
        }

        public bool Equals(Product? other)
        {
            if (other == null) return false;

            return this.Name == other.Name &&
                   this.Price == other.Price &&
                   this.Quantity == other.Quantity &&
                   this.Sum == other.Sum &&
                   this.Category == other.Category;
        }

        public static bool operator ==(Product p1, Product p2)
        {
            if (object.ReferenceEquals(p1, null))
            {
                if (object.ReferenceEquals(p2, null))
                {
                    return true;
                }
                else return false;
            }

            return p1.Equals(p2);
        }

        public static bool operator !=(Product p1, Product p2)
        {
            return !(p1 == p2);
        }

    }
}
