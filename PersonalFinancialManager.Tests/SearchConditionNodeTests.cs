using PersonalFinancialManager.source;
using PersonalFinancialManager.source.Forms;

namespace PersonalFinancialManager.Tests
{
    public class SearchConditionNodeTests
    {
            // -----------------------------
            // Constructor + SetCondition
            // -----------------------------


            [Fact]
            public void Constructor_NumberValue_CreatesCorrectCondition()
            {
                var node = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                Assert.Equal("Price > 100", node.Condition);
                Assert.Equal(SearchConditionNode.ConditionConnectionType.NONE, node.ConnectionType);
            }

            [Fact]
            public void Constructor_StringValue_UsesLikePattern()
            {
                var node = new SearchConditionNode(
                    "Name", "=", "Milk",
                    GetNewConditionForm.AttributeType.STRING
                );

                Assert.Equal("Name = '%Milk%'", node.Condition);
            }

            [Fact]
            public void Constructor_DateTimeValue_UsesQuotes()
            {
                var node = new SearchConditionNode(
                    "Date", ">=", "2024-01-01",
                    GetNewConditionForm.AttributeType.DATETIME
                );

                Assert.Equal("Date >= '2024-01-01'", node.Condition);
            }

            // -----------------------------
            // IsEmpty
            // -----------------------------

            [Fact]
            public void IsEmpty_SingleCondition_ReturnsFalse()
            {
                var node = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                Assert.False(node.IsEmpty());
            }

            [Fact]
            public void IsEmpty_NodeWithConnectionButNoChildren_ReturnsTrue()
            {
                var node = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                node.Set(SearchConditionNode.ConditionConnectionType.AND);

                Assert.True(node.IsEmpty());
            }

            // -----------------------------
            // Set (AND / OR)
            // -----------------------------

            [Fact]
            public void Set_AND_WithSecondNode_CreatesTree()
            {
                var root = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                var second = new SearchConditionNode(
                    "Category", "=", "Food",
                    GetNewConditionForm.AttributeType.STRING
                );

                root.Set(SearchConditionNode.ConditionConnectionType.AND, second);

                Assert.Equal(SearchConditionNode.ConditionConnectionType.AND, root.ConnectionType);
                Assert.NotNull(root.SearchConditionNodes);
                Assert.Equal(2, root.SearchConditionNodes.Count);
            }

            [Fact]
            public void Set_AND_WithNullNode_DoesNotCrash()
            {
                var root = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                root.Set(SearchConditionNode.ConditionConnectionType.AND, null);

                Assert.Equal(SearchConditionNode.ConditionConnectionType.AND, root.ConnectionType);
            }

            [Fact]
            public void Set_NONE_ReplacesConditionFromNode()
            {
                var root = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                var newNode = new SearchConditionNode(
                    "Amount", "<", "50",
                    GetNewConditionForm.AttributeType.INT
                );

                root.Set(SearchConditionNode.ConditionConnectionType.NONE, newNode);

                Assert.Equal("Amount < 50", root.Condition);
                Assert.Equal(SearchConditionNode.ConditionConnectionType.NONE, root.ConnectionType);
                Assert.Null(root.SearchConditionNodes);
            }

            // -----------------------------
            // GetConditionsString
            // -----------------------------

            [Fact]
            public void GetConditionsString_SingleCondition_ReturnsCondition()
            {
                var node = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                var sql = node.GetConditionsString();

                Assert.Equal("Price > 100", sql);
            }

            [Fact]
            public void GetConditionsString_AND_ReturnsGroupedExpression()
            {
                var root = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                var second = new SearchConditionNode(
                    "Category", "=", "Food",
                    GetNewConditionForm.AttributeType.STRING
                );

                root.Set(SearchConditionNode.ConditionConnectionType.AND, second);

                var sql = root.GetConditionsString();

                Assert.Equal("(Price > 100 AND Category = '%Food%')", sql);
            }

            // -----------------------------
            // GetConnectionTypeAsString
            // -----------------------------

            [Fact]
            public void GetConnectionTypeAsString_AND()
            {
                var node = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                node.Set(SearchConditionNode.ConditionConnectionType.AND);

                Assert.Equal(" AND ", node.GetConnectionTypeAsString());
            }

            [Fact]
            public void GetConnectionTypeAsString_NONE_ReturnsEmpty()
            {
                var node = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                Assert.Equal(string.Empty, node.GetConnectionTypeAsString());
            }

            // -----------------------------
            // Delete
            // -----------------------------

            [Fact]
            public void Delete_RootNode_RemovesTree()
            {
                SearchConditionNode root = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                var result = SearchConditionNode.Delete(ref root);

                Assert.True(result);
                Assert.Null(root);
            }

            [Fact]
            public void Delete_ChildNode_LeavesSingleCondition()
            {
                var root = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                var second = new SearchConditionNode(
                    "Category", "=", "Food",
                    GetNewConditionForm.AttributeType.STRING
                );

                root.Set(SearchConditionNode.ConditionConnectionType.AND, second);

                var result = SearchConditionNode.Delete(ref root, second);

                Assert.True(result);
                Assert.Equal(SearchConditionNode.ConditionConnectionType.NONE, root.ConnectionType);
                Assert.Equal("Price > 100", root.Condition);
            }

            [Fact]
            public void Delete_NonExistingNode_ReturnsFalse()
            {
                var root = new SearchConditionNode(
                    "Price", ">", "100",
                    GetNewConditionForm.AttributeType.INT
                );

                var fakeNode = new SearchConditionNode(
                    "Fake", "=", "123",
                    GetNewConditionForm.AttributeType.INT
                );

                var result = SearchConditionNode.Delete(ref root, fakeNode);

                Assert.False(result);
            }
        }
}