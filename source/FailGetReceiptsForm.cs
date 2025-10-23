using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PersonalFinancialManager.source.FTSDecodingReceiptsResult;

namespace PersonalFinancialManager.source
{
    public partial class FailGetReceiptsForm : Form
    {
        public FailGetReceiptsForm(List<FailGettingReceiptData> fails)
        {
            InitializeComponent();

            foreach (FailGettingReceiptData fail in fails)
            {
                failGetReceiptsTree.Nodes.Add(new TreeNode(
                    $"QR: {fail.FileName}",
                    new TreeNode[] { new TreeNode(FailGettingReceiptData.CodeToString(fail.Code))}));
            }
        }
    }
}
