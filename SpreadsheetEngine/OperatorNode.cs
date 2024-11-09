// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
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
    /// Abstract class for a node that calculates an operation.
    /// </summary>
    internal abstract class OperatorNode : Node
    {
        protected char opChar;
        protected Node left;
        protected Node right;

        public int Precedence { get; private set; }

        /// <summary>
        /// Initializes a new instance of the OperatorNode class with the specified operator character.
        /// </summary>
        /// <param name="opChar">The operator character.</param>
        /// <param name="left">The left child node.</param>
        /// <param name="right">The right child node.</param>
        /// <param name="precedence">The precedence value of the operator.</param>
        public OperatorNode(char opChar, Node left, Node right, int precedence)
        {
            this.opChar = opChar;
            this.left = left;
            this.right = right;
            this.Precedence = precedence;
        }

        /// <summary>
        /// Abstract method for evaluating the operation of the node.
        /// </summary>
        /// <param name="variables">Dictionary of variable values.</param>
        /// <returns>Evaluated value.</returns>
        public abstract override double Evaluate(Dictionary<string, double> variables);
    }
}