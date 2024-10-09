using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class ExpressionTree
    {
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private Node root;

        public ExpressionTree(string expression) 
        {

        }

        public void SetVariable(string variableName, double variableValue)
        {
        }

        public double Evaluate()
        {
            return 0.0;
        }
    }
}
