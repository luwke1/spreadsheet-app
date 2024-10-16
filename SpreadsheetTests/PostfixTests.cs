namespace SpreadsheetEngine
{
    using System.Windows.Forms;

    public class PostfixTests
    {
        [Test]
        public void ConvertToPostfix_SimpleAddition()
        {
            string expression = "5+10";
            ExpressionTree tree = new ExpressionTree(expression);

            Queue<string> tokens = tree.Tokenize(expression);
            Queue<string> postfixTokens = tree.ConvertToPostfix(tokens);

            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("5"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("10"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("+"));
        }

        [Test]
        public void ConvertToPostfix_WithParentheses()
        {
            string expression = "(5+3)*2";
            ExpressionTree tree = new ExpressionTree(expression);

            Queue<string> tokens = tree.Tokenize(expression);
            Queue<string> postfixTokens = tree.ConvertToPostfix(tokens);

            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("5"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("3"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("+"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("2"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("*"));
        }

        [Test]
        public void ConvertToPostfix_OperatorPrecedence()
        {
            string expression = "5+3*2";
            ExpressionTree tree = new ExpressionTree(expression);

            Queue<string> tokens = tree.Tokenize(expression);
            Queue<string> postfixTokens = tree.ConvertToPostfix(tokens);

            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("5"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("3"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("2"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("*"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("+"));
        }

        [Test]
        public void ConvertToPostfix_NestedParentheses()
        {
            string expression = "((5+3)*2)-7";
            ExpressionTree tree = new ExpressionTree(expression);

            Queue<string> tokens = tree.Tokenize(expression);
            Queue<string> postfixTokens = tree.ConvertToPostfix(tokens);

            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("5"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("3"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("+"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("2"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("*"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("7"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("-"));
        }

        [Test]
        public void ConvertToPostfix_WithVariables()
        {
            string expression = "A1+5*B2";
            ExpressionTree tree = new ExpressionTree(expression);

            Queue<string> tokens = tree.Tokenize(expression);
            Queue<string> postfixTokens = tree.ConvertToPostfix(tokens);

            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("A1"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("5"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("B2"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("*"));
            Assert.That(postfixTokens.Dequeue(), Is.EqualTo("+"));
        }
    }
}