using System;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Factory class to create operator nodes based on the operator character.
    /// </summary>
    internal class OperatorNodeFactory
    {

        public OperatorNodeFactory()
        {
            
        }

        /// <summary>
        /// Creates an operator node based on the operator character.
        /// </summary>
        /// <param name="op">The operator character.</param>
        /// <param name="left">The left operand node.</param>
        /// <param name="right">The right operand node.</param>
        /// <returns>The operator node.</returns>
        public OperatorNode CreateOperatorNode(string op, Node left, Node right)
        {
            switch (op)
            {
                case "+":
                    return new AdditionNode(left, right);
                case "-":
                    return new SubtractionNode(left, right);
                case "*":
                    return new MultiplicationNode(left, right);
                case "/":
                    return new DivisionNode(left, right);
                default:
                    throw new InvalidOperationException("Invalid operator: " + op);
            }
        }
    }
}