using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class for an Multiplication node in the expression tree.
    /// </summary>
    internal class MultiplicationNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the MultiplicationNode class.
        /// </summary>
        /// <param name="left">The left child node.</param>
        /// <param name="right">The right child node.</param>
        public MultiplicationNode(Node left, Node right)
            : base('*', left, right)
        {
        }

        /// <summary>
        /// Evaluates the Multiplication operation of the node.
        /// </summary>
        /// <param name="variables">Dictionary of variable values.</param>
        /// <returns>Evaluated value.</returns>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            return this.left.Evaluate(variables) * this.right.Evaluate(variables);
        }
    }
}