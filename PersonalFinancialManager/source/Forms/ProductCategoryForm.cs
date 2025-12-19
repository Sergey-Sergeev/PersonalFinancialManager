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
    public partial class ProductCategoryForm : Form
    {
        public bool IsOk = false;
        public ProductCategory? OutProductCategory;

        public ProductCategoryForm(Product product)
        {
            InitializeComponent();

            foreach (string category in ProductCategory.AllCategories)
                categoryComboBox.Items.Add(category);


            productNameTextBox.Text = product.Name;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            IsOk = true;

            if (categoryComboBox.Text == String.Empty)
                OutProductCategory = new ProductCategory();
            else OutProductCategory = new ProductCategory(categoryComboBox.Text);

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setAutoButton_Click(object sender, EventArgs e)
        {
            categoryComboBox.Text = ProductCategory.AutoSetProductCategory(productNameTextBox.Text).Name;
        }
    }
}
