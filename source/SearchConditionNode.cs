using PersonalFinancialManager.source.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenCvSharp.ML.DTrees;

namespace PersonalFinancialManager.source
{
    public class SearchConditionNode
    {
        private const string SQL_AND = " AND ";
        private const string SQL_OR = " OR ";
        private const string SQL_LIKE = "LIKE";


        public ConditionConnectionType ConnectionType { get; private set; }
        public List<SearchConditionNode>? SearchConditionNodes;
        public string? Condition { get; private set; }

        public string? Attribute { get; private set; }
        public string? OperatorString { get; private set; }
        public string? Value { get; private set; }
        public GetNewConditionForm.AttributeType ValueType;

        public SearchConditionNode(string attribute, string operatorString, string value, GetNewConditionForm.AttributeType valueType)
        {
            ConnectionType = ConditionConnectionType.NONE;
            SearchConditionNodes = null;
            ValueType = valueType;
            Attribute = attribute;
            OperatorString = operatorString;
            Value = value;
            SetCondition(attribute, operatorString, value);
        }

        public bool IsEmpty()
        {
            return (ConnectionType == SearchConditionNode.ConditionConnectionType.NONE && Condition == null) ||
                (ConnectionType != SearchConditionNode.ConditionConnectionType.NONE &&
                (SearchConditionNodes == null || SearchConditionNodes?.Count == 1));
        }


        private void SetCondition(string attribute, string operatorString, string value)
        {
            if(ValueType == GetNewConditionForm.AttributeType.DATETIME)
                Condition = $"{attribute} {operatorString} '{value}'";
            else if(ValueType == GetNewConditionForm.AttributeType.STRING)
                Condition = $"{attribute} {operatorString} '%{value}%'";
            else Condition = $"{attribute} {operatorString} {value}";
        }

        public void Set(ConditionConnectionType type, SearchConditionNode? node = null)
        {
            if (type == ConditionConnectionType.NONE)
            {
                if (node != null)
                {
                    SearchConditionNodes?.Clear();
                    ConnectionType = ConditionConnectionType.NONE;
                    SearchConditionNodes = null;
                    Attribute = node.Attribute;
                    OperatorString = node.OperatorString;
                    Value = node.Value;
                    ValueType = node.ValueType;
                    SetCondition(node.Attribute, node.OperatorString, node.Value);                    
                }

                return;
            }

            ConnectionType = type;

            if (SearchConditionNodes != null)
            {
                if (node != null)
                {
                    SearchConditionNodes.Add(node);
                }

                return;
            }

            SearchConditionNodes = new List<SearchConditionNode>();
            SearchConditionNodes.Add(new SearchConditionNode(Attribute, OperatorString, Value, ValueType));
            Condition = null;

            if (node != null)
                SearchConditionNodes.Add(node);
        }

        public static bool Delete(ref SearchConditionNode? root, SearchConditionNode? node = null)
        {
            if (node == null)
            {
                root = null;
                return true;
            }

            if (root == null)
                return false;

            if (ReferenceEquals(root, node))
            {
                root = null;
                return true;
            }

            return DeleteInternal(root, node);
        }

        private static bool DeleteInternal(SearchConditionNode parent, SearchConditionNode target)
        {
            if (parent.SearchConditionNodes == null)
                return false;

            for (int i = 0; i < parent.SearchConditionNodes.Count; i++)
            {
                var child = parent.SearchConditionNodes[i];

                if (ReferenceEquals(child, target))
                {
                    parent.SearchConditionNodes.RemoveAt(i);

                    if (parent.SearchConditionNodes.Count == 1)
                    {
                        if (parent.SearchConditionNodes[0].ConnectionType != ConditionConnectionType.NONE)
                        {
                            parent.Attribute = parent.SearchConditionNodes[0].Attribute;
                            parent.Value = parent.SearchConditionNodes[0].Value;
                            parent.OperatorString = parent.SearchConditionNodes[0].OperatorString;
                            parent.ConnectionType = parent.SearchConditionNodes[0].ConnectionType;
                            parent.SearchConditionNodes = parent.SearchConditionNodes[0].SearchConditionNodes;
                        }
                        else
                        {
                            parent.Condition = parent.SearchConditionNodes[0].Condition;
                            parent.SearchConditionNodes.Clear();
                            parent.SearchConditionNodes = null;
                            parent.ConnectionType = SearchConditionNode.ConditionConnectionType.NONE;
                        }
                    }

                    return true;
                }

                if (DeleteInternal(child, target))
                    return true;
            }

            return false;
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
