using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManager.source
{
    public class ProductCategory
    {
        public static HashSet<string> AllCategories { get; private set; } = new HashSet<string>();
        public string Name { get; private set; }

        const string UNKNOWN_CATEGORY = "unknown";

        public static void SetAllCategories(ref Database database)
        {
            AllCategories = database.GetAllUniqueProductCategories().ToHashSet();
        }

        public ProductCategory()
        {
            Name = UNKNOWN_CATEGORY;
        }

        public ProductCategory(string name)
        {
            if(!AllCategories.Contains(name))
                AllCategories.Add(name);

            Name = name;
        }

        public static bool operator ==(ProductCategory c1, ProductCategory c2)
        {
            return c1.Name == c2.Name;
        }

        public static bool operator !=(ProductCategory c1, ProductCategory c2)
        {
            return c1.Name != c2.Name;
        }


        public static ProductCategory AutoSetProductCategory(string productName)
        {
            ProductCategory category  = new ProductCategory();




            //      TO DO: realize auto setter



            return category;
        }

    }
}
