using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Abstract class for a node that calculates an operation.
    /// </summary>
    internal abstract class OperatorNode : Node
    {
        protected char opChar;
        protected Node left;
        protected Node right;

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

        /// <summary>
        /// Abstract method for evaluating the operation of the node.
        /// </summary>
        /// <param name="variables">Dictionary of variable values.</param>
        /// <returns>Evaluated value.</returns>
        public abstract override double Evaluate(Dictionary<string, double> variables);
    }
}