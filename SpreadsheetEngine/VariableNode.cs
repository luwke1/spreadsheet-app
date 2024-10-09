using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class for a node that represents a variable.
    /// </summary>
    internal class VariableNode : Node
    {
        private string name;

        public VariableNode(string name, double value=0.0)
        {
            this.name = name;
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
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
