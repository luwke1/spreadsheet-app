// <copyright file="VariableNode.cs" company="PlaceholderCompany">
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
    /// Class for a node that represents a variable.
    /// </summary>
    internal class VariableNode : Node
    {
        private string name;

        /// <summary>
        /// Initializes a new instance of the VariableNode class with a variable name.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">Value of variable.</param>
        public VariableNode(string name, double value = 0.0)
        {
            this.name = name;
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
            // Returns the value associated with the VariableNodes name
            if (variables != null && variables.ContainsKey(this.name))
            {
                return variables[this.name];
            }
            else
            {
                return 0.0;
            }
        }
    }
}
