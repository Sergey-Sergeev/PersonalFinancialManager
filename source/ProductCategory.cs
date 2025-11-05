using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManager.source
{
    public class ProductCategory
    {
        public static HashSet<string> AllCategories = new HashSet<string>();
        public string CategoryName { get; private set; }

        const string UNKNOWN_CATEGORY = "unknown";

        public ProductCategory()
        {
            CategoryName = UNKNOWN_CATEGORY;
        }

        public ProductCategory(string name)
        {
            CategoryName = name;
        }

        public static bool operator ==(ProductCategory c1, ProductCategory c2)
        {
            return c1.CategoryName == c2.CategoryName;
        }

        public static bool operator !=(ProductCategory c1, ProductCategory c2)
        {
            return c1.CategoryName != c2.CategoryName;
        }


        public static ProductCategory AutoSetProductCategory(string productName)
        {
            ProductCategory category  = new ProductCategory();




            //      TO DO: realize auto setter



            return category;
        }

    }
}
