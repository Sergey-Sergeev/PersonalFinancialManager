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
    public partial class GetNewConditionForm : Form
    {
        public bool IsOk = false;
        public SearchConditionNode? OutNode = null;
        private Dictionary<string, string> curAttributes;

        private Database.EntityType type;

        private readonly Dictionary<string, string> receiptAttributes = new Dictionary<string, string>()
        {
            { "ID чека", Database.ReceiptDBNames.ID },
            { "Дата и время", Database.ReceiptDBNames.DATE_AND_TIME },
            { "Адрес", Database.ReceiptDBNames.ADDRESS },
            { "Вся сумма", Database.ReceiptDBNames.TOTAL_SUM },
            { "Сумма наличными", Database.ReceiptDBNames.CASH_SUM },
            { "Сумма картой", Database.ReceiptDBNames.E_CASH_SUM }
        };

        private readonly Dictionary<string, string> productAttributes = new Dictionary<string, string>()
        {
            { "ID продукта", Database.ProductDBNames.ID },
            //{ "ID чека", Database.ProductDBNames.RECEIPT_ID },
            { "Название", Database.ProductDBNames.NAME },
            { "Цена", Database.ProductDBNames.PRICE },
            { "Количество", Database.ProductDBNames.QUANTITY },
            { "Сумма", Database.ProductDBNames.SUM },
            { "Категория", Database.ProductDBNames.CATEGORY }
        };

        private readonly Dictionary<string, string> operatorsPairs = new Dictionary<string, string>()
        {
            { "  >", ">" },
            { "  <", "<" },
            { "  =", "=" },
            { "  >=", ">=" },
            { "  <=", "<=" },
            { "  <>", "<>" }
        };

        private readonly KeyValuePair<string, string> stringOnlyOperatorsPair = new KeyValuePair<string, string>("\t~", "LIKE");


        public GetNewConditionForm(Database.EntityType type, SearchConditionNode? curCondition = null)
        {
            InitializeComponent();

            this.type = type;

            if (type == Database.EntityType.Receipt)
            {
                curAttributes = receiptAttributes;
                entityTextBox.Text = "Чек";

                foreach (KeyValuePair<string, string> pair in receiptAttributes)
                    attributeComboBox.Items.Add(pair.Key);
            }
            else
            {
                curAttributes = productAttributes;
                entityTextBox.Text = "Продукт";

                foreach (KeyValuePair<string, string> pair in productAttributes)
                    attributeComboBox.Items.Add(pair.Key);
            }

            if (curCondition != null)
            {
                attributeComboBox.Text = GetKeyByValue(curCondition.Attribute, curAttributes);
                operatorComboBox.Text = GetKeyByValue(curCondition.OperatorString, curAttributes);
                valueTextBox.Text = curCondition.Value;
            }

            foreach (KeyValuePair<string, string> pair in operatorsPairs)
                operatorComboBox.Items.Add(pair.Key);
        }

        private string GetKeyByValue(string value, Dictionary<string, string> dictionary)
        {
            for (int i = 0; i < dictionary.Keys.Count; i++)
            {
                if (dictionary.Values.ElementAt(i) == value)
                {
                    return dictionary.Keys.ElementAt(i);
                }
            }

            return String.Empty;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (attributeComboBox.Text != String.Empty &&
                operatorComboBox.Text != String.Empty &&
                valueTextBox.Text != String.Empty)
            {
                string oper = operatorComboBox.Text;
                string value = valueTextBox.Text;

                if (GetAttributeType(attributeComboBox.Text) == AttributeType.STRING ||
                    GetAttributeType(attributeComboBox.Text) == AttributeType.DATETIME)
                {
                    value = $"'{value}'";

                }

                if (stringOnlyOperatorsPair.Key == oper)
                    oper = stringOnlyOperatorsPair.Value;
                else
                {
                    oper = operatorsPairs[oper];
                }

                OutNode = new SearchConditionNode(
                    curAttributes[attributeComboBox.Text],
                    oper,
                    value);
                IsOk = true;
                Close();
            }
        }

        private AttributeType GetAttributeType(string attribute)
        {
            string valueAttribute;

            if (!productAttributes.TryGetValue(attribute, out valueAttribute))
                valueAttribute = receiptAttributes[attribute];

            if (
                valueAttribute == Database.ReceiptDBNames.ID ||
                valueAttribute == Database.ProductDBNames.ID ||
                valueAttribute == Database.ProductDBNames.RECEIPT_ID
                )
                return AttributeType.INT;
            else if (
                    valueAttribute == Database.ReceiptDBNames.TOTAL_SUM ||
                    valueAttribute == Database.ReceiptDBNames.CASH_SUM ||
                    valueAttribute == Database.ReceiptDBNames.E_CASH_SUM ||

                    valueAttribute == Database.ProductDBNames.PRICE ||
                    valueAttribute == Database.ProductDBNames.SUM ||
                    valueAttribute == Database.ProductDBNames.QUANTITY
                )
                return AttributeType.DOUBLE;
            else if (valueAttribute == Database.ReceiptDBNames.DATE_AND_TIME)
                return AttributeType.DATETIME;
            else return AttributeType.STRING;
        }


        private void valueTextBox_Leave(object sender, EventArgs e)
        {
            if (attributeComboBox.Text == String.Empty)
            {
                valueTextBox.Text = String.Empty;
                return;
            }

            AttributeType attributeType = GetAttributeType(attributeComboBox.Text);


            if (attributeType == AttributeType.DATETIME)
            {
                if (DateTime.TryParse(valueTextBox.Text, out DateTime v))
                    valueTextBox.Text = Database.ConvertDateTimeToSqlFormat(v);
                else valueTextBox.Text = String.Empty;
            }
            else if (attributeType == AttributeType.INT)
            {
                if (!int.TryParse(valueTextBox.Text, out int value))
                    valueTextBox.Text = String.Empty;
            }
            else if (attributeType == AttributeType.DOUBLE)
            {
                if (double.TryParse(valueTextBox.Text.Replace(".", ","), out double value))
                    valueTextBox.Text = value.ToString().Replace(",", ".");
                else valueTextBox.Text = String.Empty;
            }
        }

        private void attributeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (attributeComboBox.Text == String.Empty)
                return;

            operatorComboBox.Items.Clear();

            foreach (KeyValuePair<string, string> pair in operatorsPairs)
                operatorComboBox.Items.Add(pair.Key);

            if (GetAttributeType(attributeComboBox.Text) == AttributeType.STRING)
                operatorComboBox.Items.Add(stringOnlyOperatorsPair.Key);
        }

        enum AttributeType
        {
            INT,
            DOUBLE,
            STRING,
            DATETIME
        }

    }
}
