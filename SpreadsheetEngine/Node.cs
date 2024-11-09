// <copyright file="Node.cs" company="PlaceholderCompany">
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
    /// Abstract base class for nodes in the expression tree.
    /// </summary>
    internal abstract class Node
    {
        /// <summary>
        /// Abstract method for evaluating the node and returns its numerical value.
        /// </summary>
        /// <param name="variables">Dictionary of variable values.</param>
        /// <returns>Evaluated value.</returns>
        public abstract double Evaluate(Dictionary<string, double> variables);
    }
}
