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
    public partial class StringQRDataForm : Form
    {
        public string? QRDataString = null;
        public StringQRDataForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void oKButton_Click(object sender, EventArgs e)
        {
            if (qrStringDataTextBox.Text != String.Empty)
            {
                QRDataString = qrStringDataTextBox.Text;
                Close();
            }
        }
    }
}
