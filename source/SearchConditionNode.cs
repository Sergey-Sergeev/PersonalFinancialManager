using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManager.source
{
    public class SearchConditionNode
    {
        private const string SQL_AND = " AND ";
        private const string SQL_OR = " OR ";
        private const string SQL_LIKE = "LIKE";

        public ConditionConnectionType ConnectionType { get; private set; }
        public List<SearchConditionNode>? SearchConditionNodes { get; private set; }
        public string? Condition { get; private set; }

        public string Attribute { get; private set; }
        public string OperatorString { get; private set; }
        public string Value { get; private set; }

        public SearchConditionNode(string attribute, string operatorString, string value)
        {
            ConnectionType = ConditionConnectionType.NONE;
            SearchConditionNodes = null;
            SetCondition(attribute, operatorString, value);
            Attribute = attribute;
            OperatorString = operatorString;
            Value = value;
        }

        public bool IsEmpty()
        {
            return (ConnectionType == SearchConditionNode.ConditionConnectionType.NONE && Condition == null) ||
                (ConnectionType != SearchConditionNode.ConditionConnectionType.NONE &&
                (SearchConditionNodes == null || SearchConditionNodes?.Count == 0));
        }


        private void SetCondition(string attribute, string operatorString, string value)
        {
            /*if (operatorString == SQL_LIKE)
                Condition = $"{attribute} {operatorString} '%{value}%'";
            else */
            Condition = $"{attribute} {operatorString} {value}";
        }

        public void Set(ConditionConnectionType type, string? attribute = null, string? operatorString = null, string? value = null)
        {
            if (type == ConditionConnectionType.NONE)
            {
                if (attribute != null && operatorString != null && value != null)
                {
                    SetCondition(attribute, operatorString, value);
                    SearchConditionNodes?.Clear();
                    ConnectionType = ConditionConnectionType.NONE;
                    SearchConditionNodes = null;
                }

                return;
            }

            ConnectionType = type;

            if (SearchConditionNodes != null)
            {
                if (attribute != null && operatorString != null && value != null)
                {
                    SearchConditionNodes.Add(new SearchConditionNode(attribute, operatorString, value));
                }

                return;
            }

            SearchConditionNodes = new List<SearchConditionNode>();
            SearchConditionNodes.Add(new SearchConditionNode(Attribute, OperatorString, Value));
            Condition = null;

            if (attribute != null && operatorString != null && value != null)
                SearchConditionNodes.Add(new SearchConditionNode(attribute, operatorString, value));
        }

        public static void Delete(ref SearchConditionNode? node)
        {
            if (node == null) return;

            if (node.ConnectionType != ConditionConnectionType.NONE)
            {
                for (int i = 0; i < node.SearchConditionNodes.Count; i++)
                {
                    SearchConditionNode other = node.SearchConditionNodes[i];
                    Delete(ref other);
                }
            }

            node.SearchConditionNodes?.Clear();
            node.SearchConditionNodes = null;
            node.Condition = null;
            node = null;
        }


        public string GetConditionsString()
        {
            return GetConditionsFromNode(this);
        }

        public string GetConnectionTypeAsString()
        {
            switch (ConnectionType)
            {
                case ConditionConnectionType.AND:
                    return SQL_AND;
                case ConditionConnectionType.OR:
                    return SQL_OR;
                case ConditionConnectionType.NONE:
                default:
                    return String.Empty;
            }
        }

        private string GetConditionsFromNode(SearchConditionNode node)
        {
            if (node.ConnectionType == ConditionConnectionType.NONE)
                return node.Condition;

            string connectionOperator;
            if (node.ConnectionType == ConditionConnectionType.AND)
                connectionOperator = SQL_AND;
            else connectionOperator = SQL_OR;


            string condition = "(";

            for (int i = 0; i < node.SearchConditionNodes.Count; i++)
            {
                condition += node.GetConditionsFromNode(node.SearchConditionNodes[i]);

                if (i != node.SearchConditionNodes.Count - 1)
                    condition += connectionOperator;
            }

            condition += ")";

            return condition;
        }



        public enum ConditionConnectionType
        {
            NONE,
            AND,
            OR
        }
    }

}
