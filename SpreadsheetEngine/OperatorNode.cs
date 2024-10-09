using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class for a node that calculates an operation.
    /// </summary>
    internal class OperatorNode : Node
    {
        private char opChar;
        private Node left;
        private Node right;

        /// <summary>
        /// Initializes a new instance of the OperatorNode class with the specified operator character.
        /// </summary>
        /// <param name="operatorChar">The operator character.</param>
        /// <param name="left">The left child node.</param>
        /// <param name="right">The right child node.</param>
        public OperatorNode(char opChar, Node left, Node right)
        {
            this.opChar = opChar;
            this.left = left;
            this.right = right;
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
            // Evaluates the left and right child nodes
            double leftVal = this.left.Evaluate(variables);
            double rightVal = this.right.Evaluate(variables);

            // Returns the outcome from combing operator and child nodes
            switch (opChar)
            {
                case '+':
                    return leftVal + rightVal;
                case '-':
                    return leftVal - rightVal;
                case '*':
                    return leftVal * rightVal;
                case '/':
                    return leftVal / rightVal;
                default:
                    throw new Exception("Invalid operator");
            }
        }
    }
}
