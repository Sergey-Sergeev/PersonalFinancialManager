using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OpenCvSharp.ML.DTrees;

namespace PersonalFinancialManager.source.Forms
{
    public partial class NewSearchConditionForm : Form
    {
        private const string RECEIPT_UI_KEY = "Чеками";
        private const string PRODUCT_UI_KEY = "Продуктами";
        public bool IsOk = false;

        public SearchConditionNode? OutRoot;
        public Database.EntityType OutEntity = Database.EntityType.Receipt;

        private string? prevEntityComboBoxText;



        private readonly Dictionary<string, string> entityPairs = new Dictionary<string, string>()
        {
            { RECEIPT_UI_KEY, Database.RECEIPTS_DATA_TABLE_NAME },            // must be the first
            { PRODUCT_UI_KEY, Database.PRODUCTS_DATA_TABLE_NAME }
        };

        public NewSearchConditionForm(SearchConditionNode? root = null)
        {
            InitializeComponent();

            foreach (KeyValuePair<string, string> pair in entityPairs)
                entityComboBox.Items.Add(pair.Key);

            if (Database.Fabric().CurrentEntityType == Database.EntityType.Receipt)
                prevEntityComboBoxText = RECEIPT_UI_KEY;
            else prevEntityComboBoxText = PRODUCT_UI_KEY;

            entityComboBox.Text = prevEntityComboBoxText;

            if (root != null && !root.IsEmpty())
            {
                OutRoot = root;
                UpdateConditionsTreeView();
            }

        }

        private void UpdateConditionsTreeView()
        {
            conditionsTreeView.Nodes.Clear();

            if (OutRoot != null)
                conditionsTreeView.Nodes.Add(CreateNewTreeViewNode(OutRoot));

            conditionsTreeView.ExpandAll();
        }

        private TreeNode CreateNewTreeViewNode(SearchConditionNode node)
        {
            if (node.ConnectionType == SearchConditionNode.ConditionConnectionType.NONE)
            {
                return new TreeNode(node.Condition);
            }

            TreeNode newTreeNode = new TreeNode(node.GetConnectionTypeAsString());

            foreach (SearchConditionNode searchNode in node.SearchConditionNodes)
            {
                newTreeNode.Nodes.Add(CreateNewTreeViewNode(searchNode));
            }

            return newTreeNode;
        }


        private void okButton_Click(object sender, EventArgs e)
        {
            if (OutRoot == null || OutRoot.IsEmpty())
                IsOk = false;
            else IsOk = true;

            Close();
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OutRoot != null && conditionsTreeView.SelectedNode != null)
            {
                SearchConditionNode node = GetNodeFromTreeView(conditionsTreeView.SelectedNode);

                if (node.ConnectionType == SearchConditionNode.ConditionConnectionType.NONE)
                {
                    GetNewConditionForm getNewConditionForm = new GetNewConditionForm(OutEntity, node);
                    getNewConditionForm.ShowDialog();


                    if (getNewConditionForm.IsOk)
                        node.Set(SearchConditionNode.ConditionConnectionType.NONE, getNewConditionForm.OutNode);
                }
                else
                {
                    if (node.ConnectionType == SearchConditionNode.ConditionConnectionType.AND)
                        node.Set(SearchConditionNode.ConditionConnectionType.OR);
                    else node.Set(SearchConditionNode.ConditionConnectionType.AND);
                }

                UpdateConditionsTreeView();
            }
        }

        private void addConditionANDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewCondition(SearchConditionNode.ConditionConnectionType.AND);
        }
        private void addConditionORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewCondition(SearchConditionNode.ConditionConnectionType.OR);
        }

        private void AddNewCondition(SearchConditionNode.ConditionConnectionType type)
        {
            GetNewConditionForm getNewConditionForm = new GetNewConditionForm(OutEntity);
            getNewConditionForm.ShowDialog();

            if (!getNewConditionForm.IsOk) return;

            if (conditionsTreeView.SelectedNode != null)
            {
                SearchConditionNode node = GetNodeFromTreeView(conditionsTreeView.SelectedNode);

                node.Set(type, getNewConditionForm.OutNode);
            }
            else
            {
                OutRoot = getNewConditionForm.OutNode;
            }

            UpdateConditionsTreeView();
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OutRoot != null && conditionsTreeView.SelectedNode != null)
            {
                SearchConditionNode temp = GetNodeFromTreeView(conditionsTreeView.SelectedNode);
                SearchConditionNode.Delete(ref OutRoot, temp);
                UpdateConditionsTreeView();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private SearchConditionNode GetNodeFromTreeView(TreeNode treeNode)
        {
            Stack<int> indexes = new Stack<int>();

            TreeNode cur = treeNode;
            while (cur.Parent != null)
            {
                indexes.Push(cur.Index);
                cur = cur.Parent;
            }

            SearchConditionNode node = OutRoot;

            while(indexes.Count != 0)
            {
                node = node.SearchConditionNodes[indexes.Pop()];
            }

            return node; 
        }


        private void clearTreeConditionsButton_Click(object? sender = null, EventArgs? e = null)
        {
            SearchConditionNode.Delete(ref OutRoot);
            UpdateConditionsTreeView();
        }


        private void entityComboBox_TextChanged(object sender, EventArgs e)
        {
            if (entityPairs[entityComboBox.Text] == Database.RECEIPTS_DATA_TABLE_NAME)
                OutEntity = Database.EntityType.Receipt;
            else OutEntity = Database.EntityType.Product;

            clearTreeConditionsButton_Click();
        }

        enum TreeViewTag
        {
            Condition,
            Connector
        }
    }
}
