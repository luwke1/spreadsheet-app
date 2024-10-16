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
        public void EvaluateExpression_Add()
        {
            string expression = "5+10";
            ExpressionTree tree = new ExpressionTree(expression);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(15.0));
        }

        [Test]
        public void EvaluateExpression_Division()
        {
            string expression = "10/5";
            ExpressionTree tree = new ExpressionTree(expression);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(2.0));
        }

        [Test]
        public void EvaluateExpression_Subtract()
        {
            string expression = "10-5";
            ExpressionTree tree = new ExpressionTree(expression);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(5.0));
        }

        [Test]
        public void EvaluateExpression_Multiply()
        {
            string expression = "10*5";
            ExpressionTree tree = new ExpressionTree(expression);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(50.0));
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
            string expression = "X1-5";
            ExpressionTree tree = new ExpressionTree(expression);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(-5.0));
        }

        [Test]
        public void EvaluateExpression_WithParentheses()
        {
            string expression = "(2+3)*5";
            ExpressionTree tree = new ExpressionTree(expression);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(25.0));
        }

        [Test]
        public void EvaluateExpression_Underflow()
        {
            string expression = "1000000000000000000-1000000000000000000";
            ExpressionTree tree = new ExpressionTree(expression);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(0.0));
        }

        [Test]
        public void EvaluateExpression_DivideByZero()
        {
            string expression = "5/0";
            ExpressionTree tree = new ExpressionTree(expression);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(double.PositiveInfinity));
        }

        [Test]
        public void EvaluateExpression_LargeVariableMultiplication()
        {
            string expression = "A1*B1";
            ExpressionTree tree = new ExpressionTree(expression);
            tree.SetVariable("A1", 1e100);
            tree.SetVariable("B1", 1e100);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(1e200));
        }

        [Test]
        public void EvaluateExpression_ComplexMixedOperations()
        {
            string expression = "(A1+B1)*(C1/D1)";
            ExpressionTree tree = new ExpressionTree(expression);
            tree.SetVariable("A1", 2);
            tree.SetVariable("B1", 3);
            tree.SetVariable("C1", 10);
            tree.SetVariable("D1", 5);
            double result = tree.Evaluate();
            Assert.That(result, Is.EqualTo(10.0));
        }
    }
}
