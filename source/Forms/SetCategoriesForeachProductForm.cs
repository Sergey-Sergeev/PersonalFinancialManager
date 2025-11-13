using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalFinancialManager.source.Forms
{
    public partial class SetCategoriesForeachProductForm : Form
    {

        private List<Product> products;

        public SetCategoriesForeachProductForm(ref List<Product> products)
        {
            InitializeComponent();
            this.products = products;

            SetListOfProducts();
        }

        private void SetListOfProducts()
        {
            listOfProductsListView.Items.Clear();

            foreach (Product product in products)
            {
                ListViewItem item = new ListViewItem(product.Name);
                item.SubItems.Add(product.Category.Name);
                listOfProductsListView.Items.Add(item);
            }
        }

        private void UpdateListOfProducts()
        {
            for (int i = 0; i < products.Count; i++)
            {
                listOfProductsListView.Items[i].SubItems[1].Text = products[i].Category.Name;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            foreach (Product product in products)
                product.Category.SetUnknown();

            Close();
        }

        private void setAutoButton_Click(object sender, EventArgs e)
        {
            foreach (Product product in products)
                product.Category = ProductCategory.AutoSetProductCategory(product.Name);

            UpdateListOfProducts();
        }

        private void listOfProductsListView_DoubleClick(object sender, EventArgs e)
        {
            if (listOfProductsListView.SelectedItems.Count != 0)
            {
                int index = listOfProductsListView.SelectedItems[0].Index;
                ProductCategoryForm productCategoryForm = new ProductCategoryForm(products[index]);
                productCategoryForm.ShowDialog();

                if (productCategoryForm.IsOk)
                {
                    products[index].Category = productCategoryForm.OutProductCategory;
                    UpdateListOfProducts();
                }
            }
        }
    }
}
