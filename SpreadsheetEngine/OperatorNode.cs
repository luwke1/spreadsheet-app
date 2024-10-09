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

        public OperatorNode(char opChar, Node left, Node right)
        {
            this.opChar = opChar;
            this.left = left;
            this.right = right;
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
            double leftVal = this.left.Evaluate(variables);
            double rightVal = this.right.Evaluate(variables);

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
