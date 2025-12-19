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
    public partial class QRDataForm : Form
    {
        private static readonly Dictionary<string, bool> receiptNtypePairs = new Dictionary<string, bool>()
        {
            { "приход", true },
            { "возврат", false }
        };

        public bool IsOk;
        public QRCodeData? OutQRData;


        public QRDataForm()
        {
            InitializeComponent();

            foreach (KeyValuePair<string, bool> pair in receiptNtypePairs)
                nComboBox.Items.Add(pair.Key);
        }

        public bool IsQRDataNotEmpty()
        {
            return fnTextBox.Text != String.Empty &&
                   iTextBox.Text != String.Empty &&
                   fpTextBox.Text != String.Empty &&
                   sTextBox.Text != String.Empty &&
                   tTextBox.Text != String.Empty &&
                   nComboBox.Text != String.Empty;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (IsQRDataNotEmpty())
            {
                IsOk = true;

                OutQRData = new QRCodeData(
                    ulong.Parse(fnTextBox.Text),
                    int.Parse(iTextBox.Text),
                    int.Parse(fpTextBox.Text),
                    Double.Parse(sTextBox.Text),
                    DateTime.Parse(tTextBox.Text),
                     receiptNtypePairs[nComboBox.Text]);

                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            IsOk = false;
            Close();
        }

        private void sTextBox_Leave(object sender, EventArgs e)
        {
            if (!Double.TryParse(sTextBox.Text, out Double dateTime))
            {
                sTextBox.Text = String.Empty;
            }
        }

        private void tTextBox_Leave(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(tTextBox.Text, out DateTime dateTime))
            {
                tTextBox.Text = String.Empty;
            }
            else tTextBox.Text = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}
