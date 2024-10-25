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
        private Spreadsheet spreadsheet;

        public List<string> Variables
        {
            get { return variables.Keys.ToList(); }
        }

        /// <summary>
        /// Initializes an ExpressionTree.
        /// </summary>
        /// <param name="expression">The expression to be built.</param>
        public ExpressionTree(string expression, Spreadsheet spreadsheetMain) 
        {
            this.root = BuildTree(expression);
            this.Expression = expression;
            this.spreadsheet = spreadsheetMain;
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

            // Converts tokens into the postfix notation
            Queue<string> postfixTokens = ConvertToPostfix(tokens);

            return BuildTreeFromPostfix(postfixTokens);
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
                    while (operatorStack.Peek() != "(")
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }

                    // pop the left parenthesis
                    operatorStack.Pop();
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
        /// Builds an expression tree from postfix notation queue.
        /// </summary>
        /// <param name="postfixTokens">A queue of tokens representing the expression in postfix notation.</param>
        /// <returns>The root node of the expression tree.</returns>
        private Node BuildTreeFromPostfix(Queue<string> postfixTokens)
        {
            var stack = new Stack<Node>();

            while (postfixTokens.Count > 0)
            {
                string token = postfixTokens.Dequeue();

                if (double.TryParse(token, out double number))
                {
                    stack.Push(new ConstantNode(number));
                }
                else if (IsOperator(token))
                {
                    if (stack.Count < 2) return null;

                    // Pop two nodes from the stack and make an operator node from them
                    var right = stack.Pop();
                    var left = stack.Pop();
                    var operatorNode = operatorNodeFactory.CreateOperatorNode(token, left, right);
                    stack.Push(operatorNode);
                }
                else
                {
                    this.variables.Add(token, 0);
                    stack.Push(new VariableNode(token));
                }
            }

            return stack.Pop();
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
        public string Evaluate()
        {
            if (root == null)
            {
                return "ERROR";
            }

            foreach (string key in this.variables.Keys)
            {
                double value;
                if (double.TryParse(this.spreadsheet.GetCellValue(key), out value))
                {
                    this.variables[key] = value;
                }
                else
                {
                    return "ERROR";
                }   
            }

            return root.Evaluate(this.variables).ToString();
        }
    }
}
