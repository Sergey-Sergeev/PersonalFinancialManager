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
    public partial class ProductForm : Form
    {
        public bool IsOk = false;
        public Product? OutProduct;

        public ProductForm(Product? product = null)
        {
            InitializeComponent();

            foreach (string category in ProductCategory.AllCategories)
            {
                categoryComboBox.Items.Add(category);
            }

            if (product != null)
            {
                nameTextBox.Text = product.Name;
                priceTextBox.Text = product.Price.ToString();
                quantityTextBox.Text = product.Quantity.ToString();
                sumTextBox.Text = product.Sum.ToString();
                categoryComboBox.Text = product.Category.Name;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            IsOk = false;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (
                nameTextBox.Text != String.Empty &&
                priceTextBox.Text != String.Empty &&
                quantityTextBox.Text != String.Empty &&
                sumTextBox.Text != String.Empty &&
                categoryComboBox.Text != String.Empty
                )
            {
                IsOk = true;

                string name = nameTextBox.Text;
                double price = Double.Parse(priceTextBox.Text);
                double quantity = Double.Parse(quantityTextBox.Text);
                double sum = Double.Parse(sumTextBox.Text);
                ProductCategory category = new ProductCategory(categoryComboBox.Text);

                OutProduct = new Product(name, price, quantity, sum, category);

                Close();
            }
        }

        private void CalculateSum()
        {
            double price;
            double quantity;

            if (!Double.TryParse(priceTextBox.Text, out price))
            {
                price = 0;
            }

            if (!Double.TryParse(quantityTextBox.Text, out quantity))
            {
                quantity = 0;
            }

            double sum = Double.Round(price * quantity, 2);

            sumTextBox.Text = sum.ToString();
        }


        private void priceTextBox_Leave(object sender, EventArgs e)
        {
            if (!Double.TryParse(priceTextBox.Text, out double val))
            {
                priceTextBox.Text = String.Empty;
            }
            else CalculateSum();
        }

        private void quantityTextBox_Leave(object sender, EventArgs e)
        {
            if (!Double.TryParse(quantityTextBox.Text, out double val))
            {
                quantityTextBox.Text = String.Empty;
            }
            else CalculateSum();
        }
    }
}
