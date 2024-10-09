using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Abstract base class for nodes in the expression tree.
    /// </summary>
    internal abstract class Node
    {
        public abstract double Evaluate(Dictionary<string, double> variables);
    }
}
