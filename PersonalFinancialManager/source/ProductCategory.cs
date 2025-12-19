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

        private const float CATEGORY_NAME_EQUAL_PERCENTAGE = 0.85f;

        public static void SetAllCategories(ref Database database)
        {
            AllCategories = database.GetAllUniqueProductCategories().ToHashSet();
        }

        public ProductCategory()
        {
            Name = UNKNOWN_CATEGORY;
        }

        public void SetUnknown()
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
            int index = productName.IndexOf(' ');
            
            if (index == -1) return new ProductCategory();


            char[] firstWord = new char[index];

            for (int i = 0; i < index; i++)            
                firstWord[i] = productName[i];
            
            string word = new string(firstWord);


            foreach (string categoryName in AllCategories)
            {
                int minCategoryEqualLen = (int)(float.Round(categoryName.Length * CATEGORY_NAME_EQUAL_PERCENTAGE));

                if (minCategoryEqualLen < 1) continue;

                if (word.Contains(categoryName.Substring(0, minCategoryEqualLen)))
                {
                    return new ProductCategory(categoryName);
                }
            }

            return new ProductCategory(word);
        }

    }
}
