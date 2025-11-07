using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalFinancialManager.source
{
    public partial class ReceiptForm : Form
    {
        private static readonly Dictionary<string, ReceiptNType> receiptNtypePairs = new Dictionary<string, ReceiptNType>()
        {
            { "приход", ReceiptNType.IN },
            { "возврат", ReceiptNType.OUT }
        };


        public bool IsCancel;
        public Receipt outReceipt;

        public ReceiptForm(Receipt? receipt = null)
        {
            InitializeComponent();

            foreach (KeyValuePair<string, ReceiptNType> pair in receiptNtypePairs)
            {
                nComboBox.Items.Add(pair.Key);
            }

            if (receipt != null)
            {
                if (QRCodeData.ParseQRCodeData(receipt.FullFtsReceiptData, out QRCodeData? qrData))
                {
                    outReceipt = receipt;
                    fnTextBox.Text = qrData.Fn;
                    iTextBox.Text = qrData.I;
                    fpTextBox.Text = qrData.Fp;
                    sTextBox.Text = qrData.S;
                    tTextBox.Text = qrData.T;
                    nComboBox.Text = qrData.N;
                }
                else
                {
                    fnTextBox.Text =
                    iTextBox.Text =
                    fpTextBox.Text =
                    sTextBox.Text =
                    tTextBox.Text =
                    nComboBox.Text = "-";
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (fnTextBox.Text != String.Empty &&
                iTextBox.Text != String.Empty &&
                fpTextBox.Text != String.Empty &&
                sTextBox.Text != String.Empty &&
                tTextBox.Text != String.Empty &&
                nComboBox.Text != String.Empty
                )
            {
                IsCancel = false;



                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            IsCancel = true;    
            Close();
        }

        private enum ReceiptNType
        {
            IN,
            OUT
        }
    }
}
