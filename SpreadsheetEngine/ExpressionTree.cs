using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private OperatorNodeFactory operatorNodeFactory = new OperatorNodeFactory();

        /// <summary>
        /// Initializes an ExpressionTree.
        /// </summary>
        /// <param name="expression">The expression to be built.</param>
        public ExpressionTree(string expression) 
        {
            this.root = BuildTree(expression);
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

            Queue<string> tokens = Tokenize(expression);

            return root;
        }

        /// <summary>
        /// Tokenizes the input expression using regex.
        /// </summary>
        /// <param name="expression">The expression to be tokenized.</param>
        /// <returns>A queue of tokens representing the expression.</returns>
        public Queue<string> Tokenize(string expression)
        {
            var tokens = new Queue<string>();
            var pattern = @"(\d+\.?\d*|[A-Z]\d+|[\+\-\*/\(\)])";
            var matches = Regex.Matches(expression, pattern);

            foreach (Match match in matches)
            {
                tokens.Enqueue(match.Value);
            }

            return tokens;
        }

        /// <summary>
        /// Converts the tokens to postfix notation
        /// </summary>
        /// <param name="tokens">The queue of tokens representing the expression.</param>
        /// <returns>A queue of tokens representing the expression in postfix notation.</returns>
        public Queue<string> ConvertToPostfix(Queue<string> tokens)
        {
            var outputQueue = new Queue<string>();
            var operatorStack = new Stack<string>();

            while (tokens.Count > 0)
            {
                string token = tokens.Dequeue();

                if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    // Loop while stack is not empty and the current item in stack is not an open parenthesis
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }

                    // Pop the left parenthesis if stack is not empty
                    if (operatorStack.Count > 0)
                    {
                        operatorStack.Pop(); // remove "(" from the stack
                    }
                    else
                    {
                        throw new InvalidOperationException("invalid equation");
                    }
                }
                else if (IsOperator(token))
                {
                    // Handle operator precedence
                    while (operatorStack.Count > 0 && IsOperator(operatorStack.Peek()) && Precedence(operatorStack.Peek()) >= Precedence(token))
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(token);
                }
                else
                {
                    outputQueue.Enqueue(token);
                }
            }

            // Pop the remaining operators onto the output queue
            while (operatorStack.Count > 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            return outputQueue;
        }

        /// <summary>
        /// Checks if the token is an operator.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>True if the token is an operator, false otherwise.</returns>
        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }

        /// <summary>
        /// Returns the precedence of the operator.
        /// </summary>
        /// <param name="op">The operator to check precedence value.</param>
        /// <returns>The precedence value of the operator.</returns>
        private int Precedence(string op)
        {
            return op == "+" || op == "-" ? 1 : 2;
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
