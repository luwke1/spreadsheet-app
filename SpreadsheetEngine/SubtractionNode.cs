// <copyright file="SubtractionNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for an subtraction node in the expression tree.
    /// </summary>
    internal class SubtractionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionNode"/> class.
        /// </summary>
        /// <param name="left">The left child node.</param>
        /// <param name="right">The right child node.</param>
        public SubtractionNode(Node left, Node right)
            : base('-', left, right, 1)
        {
        }

        /// <summary>
        /// Gets the operator symbol for the subtraction operation.
        /// </summary>
        public static char Operator => '-';

        /// <summary>
        /// Evaluates the subtraction operation of the node.
        /// </summary>
        /// <param name="variables">Dictionary of variable values.</param>
        /// <returns>Evaluated value.</returns>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            return this.left.Evaluate(variables) - this.right.Evaluate(variables);
        }
    }
}