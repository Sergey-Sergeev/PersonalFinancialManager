using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PersonalFinancialManager.source.JsonServerClass;

namespace PersonalFinancialManager.source.Forms
{
    public partial class ReceiptForm : Form
    {
        public bool IsOk = false;
        public Receipt? OutReceipt;

        private List<Product> products = new List<Product>();

        private Receipt? inputReceipt;

        public ReceiptForm(Receipt? receipt = null)
        {
            InitializeComponent();

            if (receipt != null)
            {
                inputReceipt = receipt;

                receiptQRDataLabel.Text = $"Данные чека: {receipt.FullFtsReceiptData}";

                addsressComboBox.Text = receipt.RetailPlaceAddress;
                dateTimeTextBox.Text = receipt.DateAndTime.ToString("dd.MM.yyyy HH:mm:ss");
                cashTextBox.Text = receipt.CashTotalSum.ToString();
                eCashTextBox.Text = receipt.EcashTotalSum.ToString();
                totalSumTextBox.Text = receipt.TotalSum.ToString();

                foreach (Product product in receipt.ListOfProducts)
                {
                    products.Add(product);
                }

                UpdateListOfProducts();
            }

            foreach (string address in Database.Fabric().GetAllUniqueAddresses())
                addsressComboBox.Items.Add(address);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (addsressComboBox.Text != String.Empty &&
                dateTimeTextBox.Text != String.Empty &&
                cashTextBox.Text != String.Empty &&
                eCashTextBox.Text != String.Empty &&
                totalSumTextBox.Text != String.Empty &&
                products.Count != 0
                )
            {
                IsOk = true;

                OutReceipt = new Receipt(
                    products,
                    Double.Parse(totalSumTextBox.Text),
                    DateTime.Parse(dateTimeTextBox.Text),
                    Double.Parse(cashTextBox.Text),
                    Double.Parse(eCashTextBox.Text),
                    addsressComboBox.Text,
                    inputReceipt != null ? inputReceipt.FullFtsReceiptData : null,
                    inputReceipt != null ? inputReceipt.Id : -1);

                Close();
            }
        }

        private void UpdateListOfProducts()
        {
            productsListView.Items.Clear();

            for (int i = 0; i < products.Count; i++)
            {
                ListViewItem item = new ListViewItem(products[i].Name);
                item.SubItems.Add(products[i].Price.ToString());
                item.SubItems.Add(products[i].Quantity.ToString());
                item.SubItems.Add(products[i].Sum.ToString());
                item.SubItems.Add(products[i].Category.Name);

                productsListView.Items.Add(item);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            IsOk = false;
            Close();
        }

        private void dateTimeTextBox_Leave(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(dateTimeTextBox.Text, out DateTime dateTime))
            {
                dateTimeTextBox.Text = String.Empty;
            }
            else dateTimeTextBox.Text = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void SetTotalSum()
        {
            double totalSum = 0;

            foreach (Product product in products)
            {
                totalSum += product.Sum;
            }

            totalSum = Double.Round(totalSum, 2);

            totalSumTextBox.Text = totalSum.ToString();

            BalanceCashAndECash();
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.ShowDialog();

            if (productForm.IsOk)
            {
                products.Add(productForm.OutProduct);
            }

            SetTotalSum();
            UpdateListOfProducts();
        }

        private void changeProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (productsListView.SelectedItems.Count != 0)
            {
                Product product = GetProductFromListView(productsListView.SelectedItems[0]);
                ProductForm productForm = new ProductForm(product);

                productForm.ShowDialog();

                if (productForm.IsOk)
                {
                    products.Remove(product);
                    products.Add(productForm.OutProduct);
                }
            }

            SetTotalSum();
            UpdateListOfProducts();
        }


        private void deleteProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (productsListView.SelectedItems.Count != 0)
            {
                products.Remove(GetProductFromListView(productsListView.SelectedItems[0]));
            }

            SetTotalSum();
            UpdateListOfProducts();
        }

        private Product GetProductFromListView(ListViewItem item)
        {
            string name = item.SubItems[0].Text;
            double price = Double.Parse(item.SubItems[1].Text);
            double quantity = Double.Parse(item.SubItems[2].Text);
            double sum = Double.Parse(item.SubItems[3].Text);
            ProductCategory category = new ProductCategory(item.SubItems[4].Text);
            return new Product(name, price, quantity, sum, category);
        }

        private void BalanceCashAndECash(bool isCashChanged = true)
        {
            double cash;
            double eCash;
            double totalSum;

            if (!Double.TryParse(cashTextBox.Text, out cash))
            {
                cash = 0;
            }

            if (!Double.TryParse(eCashTextBox.Text, out eCash))
            {
                eCash = 0;
            }

            if (!Double.TryParse(totalSumTextBox.Text, out totalSum))
            {
                totalSum = 0;
            }

            if (isCashChanged)
                eCash = Double.Round(totalSum - cash, 2);
            else cash = Double.Round(totalSum - eCash, 2);

            if (eCash < 0 || cash < 0)
            {
                cash = 0;
                eCash = totalSum;
            }

            cashTextBox.Text = cash.ToString();
            eCashTextBox.Text = eCash.ToString();
            totalSumTextBox.Text = totalSum.ToString();
        }

        private void eCashTextBox_Leave(object sender, EventArgs e)
        {
            if (totalSumTextBox.Text == String.Empty)
                eCashTextBox.Text = String.Empty;

            BalanceCashAndECash(false);
        }

        private void cashTextBox_Leave(object sender, EventArgs e)
        {
            if (totalSumTextBox.Text == String.Empty)
                cashTextBox.Text = String.Empty;

            BalanceCashAndECash(true);
        }
    }
}
