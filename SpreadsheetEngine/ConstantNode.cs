﻿// <copyright file="ConstantNode.cs" company="PlaceholderCompany">
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
    /// Class for a node that represents a constant value.
    /// </summary>
    internal class ConstantNode : Node
    {
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value">The constant value.</param>
        public ConstantNode(double value)
        {
            this.value = value;
        }

        /// <inheritdoc/>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            return this.value;
        }
    }
}
