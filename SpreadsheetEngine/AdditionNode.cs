// <copyright file="AdditionNode.cs" company="PlaceholderCompany">
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
    /// Class for an addition node in the expression tree.
    /// </summary>
    /// <remarks>
    /// Initializes a <see langword="new"/> instance of the AdditionNode class.
    /// </remarks>
    /// <param name="left">The left child node.</param>
    /// <param name="right">The right child node.</param>
    internal class AdditionNode(Node left, Node right) : OperatorNode('+', left, right, 1)
    {
        /// <summary>
        /// Gets the operator symbol for the addition operation.
        /// </summary>
        public static char Operator => '+';

        /// <summary>
        /// Evaluates the addition operation of the node.
        /// </summary>
        /// <param name="variables">Dictionary of variable values.</param>
        /// <returns>Evaluated value.</returns>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            return this.left.Evaluate(variables) + this.right.Evaluate(variables);
        }
    }
}