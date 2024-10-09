using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class for a node that represents a constant value.
    /// </summary>
    internal class ConstantNode : Node
    {
        private double value;

        public ConstantNode(double value)
        {
            this.value = value;
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
            return this.value;
        }
    }
}
