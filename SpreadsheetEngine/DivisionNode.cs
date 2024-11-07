// <copyright file="DivisionNode.cs" company="PlaceholderCompany">
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
    /// Class for an DivisionNode in the expression tree.
    /// </summary>
    internal class DivisionNode : OperatorNode
    {
        /// <summary>
        /// Gets the operator symbol for the division operation.
        /// </summary>
        public static char Operator => '/';

        /// <summary>
        /// Initializes a new instance of the DivisionNode class.
        /// </summary>
        /// <param name="left">The left child node.</param>
        /// <param name="right">The right child node.</param>
        public DivisionNode(Node left, Node right)
            : base('/', left, right,2)
        {
        }

        /// <summary>
        /// Evaluates the division operation of the node.
        /// </summary>
        /// <param name="variables">Dictionary of variable values.</param>
        /// <returns>Evaluated value.</returns>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            double denominator = this.right.Evaluate(variables);
            if (denominator == 0)
            {
                throw new DivideByZeroException("Division by zero is not allowed.");
            }

            return this.left.Evaluate(variables) / denominator;
        }
    }
}