using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Represents an expression tree that can parse and evaluate expressions.
    /// </summary>
    public class ExpressionTree
    {
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private Node root;

        /// <summary>
        /// Initializes an ExpressionTree.
        /// </summary>
        /// <param name="expression">The expression to be built.</param>
        public ExpressionTree(string expression) 
        {
            root = BuildTree(expression);
            this.Expression = expression;
        }

        /// <summary>
        /// Builds an expression tree based on the specified expression string.
        /// </summary>
        /// <param name="expression">The expression string to be turned into a expression tree</param>
        /// <returns>The expression tree node that represents the specified expression.</returns>
        private Node BuildTree(string expression)
        {
            // Return null if the expression is empty
            if (expression == null || expression == "")
            {
                return null;
            }

            // Finds which operator is used in the expression string
            char[] operators = { '+', '-', '*', '/'};
            char operChar = '\0';
            foreach (char oper in operators)
            {
                if (expression.Contains(oper))
                {
                    operChar = oper;
                }
            }

            // If no operator, then the expression is a single variable or constant
            if (operChar == '\0')
            {
                // Return a constant node with the double value, otherwise return the variable node
                if (Double.TryParse(expression, out double value))
                {
                    return new ConstantNode(value);
                }
                else
                {
                    return new VariableNode(expression);
                }
            }
            else
            {
                // Get first index of operator
                int operIndex = expression.IndexOf(operChar);

                // Split the expression at first operator index, saving left and right
                string leftExpression = expression.Substring(0, operIndex);
                string rightExpression = expression.Substring(operIndex + 1);

                // Build an expression tree for left and right expressions recursively
                Node nodeLeft = BuildTree(leftExpression);
                Node nodeRight = BuildTree(rightExpression);

                // Return the new tree with left and right child trees
                return new OperatorNode(operChar, nodeLeft, nodeRight);
            }
        }

        public string Expression { get; set; }

        /// <summary>
        /// Sets the name value pairs into the variables dictionary
        /// </summary>
        /// <param name="variableName">The variable name.</param>
        /// <param name="variableValue">The variable value.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            variables.Add(variableName, variableValue);
        }

        /// <summary>
        /// Evaluates the expression tree from the root, returns its value
        /// </summary>
        /// <returns>The evaluated value of the expression tree</returns>
        public double Evaluate()
        {
            return root.Evaluate(this.variables);
        }
    }
}
