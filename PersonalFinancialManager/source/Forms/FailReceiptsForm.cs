using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PersonalFinancialManager.source.TryGetReceiptsResultUnit;

namespace PersonalFinancialManager.source.Forms
{
    public partial class FailReceiptsForm : Form
    {
        public FailReceiptsForm(List<FailData> fails)
        {
            InitializeComponent();

            foreach (FailData fail in fails)
            {
                int i = failGetReceiptsTree.Nodes.Add(new TreeNode(
                    $"QR: {fail.FileName}",
                    new TreeNode[] { new TreeNode(FailData.CodeToString(fail.Code)) })                
                );

                failGetReceiptsTree.Nodes[i].ExpandAll();
            }
        }
    }
}
