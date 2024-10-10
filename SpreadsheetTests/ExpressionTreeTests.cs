using SpreadsheetEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class ExpressionTreeTests
    {
        [Test]
        public void EvaluateExpression_Normal()
        {
            string expression = "5+10";
            ExpressionTree tree = new ExpressionTree(expression);

            double result = tree.Evaluate();

            Assert.That(result, Is.EqualTo(15.0));
        }

        [Test]
        public void EvaluateExpression_Variable()
        {
            string expression = "A1";
            ExpressionTree tree = new ExpressionTree(expression);
            tree.SetVariable("A1", 7);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(7.0));
        }

        [Test]
        public void EvaluateExpression_MultipleVariables()
        {
            string expression = "A1+B1";
            ExpressionTree tree = new ExpressionTree(expression);
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 10);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(15.0));
        }

        [Test]
        public void EvaluateEmptyVariable()
        {
            string expression = "X1+5";
            ExpressionTree tree = new ExpressionTree(expression);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(5.0));
        }
    }
}
