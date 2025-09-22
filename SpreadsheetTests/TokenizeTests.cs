namespace SpreadsheetEngine
{
    using System.Windows.Forms;

    public class TokenizeTests
    {
        [Test]
        public void Tokenize_SimpleAddition()
        {
           string expression = "5+10";
           ExpressionTree tree = new ExpressionTree(expression);

           Queue<string> tokens = tree.Tokenize(expression);

           Assert.That(tokens.Dequeue(), Is.EqualTo("5"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("+"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("10"));
        }

        [Test]
        public void Tokenize_MixedOperators()
        {
           string expression = "5*3-2/6";
           ExpressionTree tree = new ExpressionTree(expression);

           Queue<string> tokens = tree.Tokenize(expression);

           Assert.That(tokens.Dequeue(), Is.EqualTo("5"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("*"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("3"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("-"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("2"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("/"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("6"));
        }

        [Test]
        public void Tokenize_WithParentheses()
        {
           string expression = "(5+3)*2";
           ExpressionTree tree = new ExpressionTree(expression);

           Queue<string> tokens = tree.Tokenize(expression);

           Assert.That(tokens.Dequeue(), Is.EqualTo("("));
           Assert.That(tokens.Dequeue(), Is.EqualTo("5"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("+"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("3"));
           Assert.That(tokens.Dequeue(), Is.EqualTo(")"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("*"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("2"));
        }

        [Test]
        public void Tokenize_VariableAndNumber()
        {
           string expression = "A1+5";
           ExpressionTree tree = new ExpressionTree(expression);

           Queue<string> tokens = tree.Tokenize(expression);

           Assert.That(tokens.Dequeue(), Is.EqualTo("A1"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("+"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("5"));
        }

        [Test]
        public void Tokenize_MultipleVariables()
        {
           string expression = "A1+B2-C3";
           ExpressionTree tree = new ExpressionTree(expression);

           Queue<string> tokens = tree.Tokenize(expression);

           Assert.That(tokens.Dequeue(), Is.EqualTo("A1"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("+"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("B2"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("-"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("C3"));
        }

        [Test]
        public void Tokenize_EmptyExpression()
        {
           string expression = "";
           ExpressionTree tree = new ExpressionTree(expression);

           Queue<string> tokens = tree.Tokenize(expression);

           Assert.That(tokens.Count, Is.EqualTo(0));
        }

        [Test]
        public void Tokenize_ExpressionWithSpaces()
        {
           string expression = "5 + 3 - 2";
           ExpressionTree tree = new ExpressionTree(expression);

           Queue<string> tokens = tree.Tokenize(expression);

           Assert.That(tokens.Dequeue(), Is.EqualTo("5"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("+"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("3"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("-"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("2"));
        }

        [Test]
        public void Tokenize_SpecialCharactersInExpression()
        {
           string expression = "5+3*2/(1-7)";
           ExpressionTree tree = new ExpressionTree(expression);

           Queue<string> tokens = tree.Tokenize(expression);

           Assert.That(tokens.Dequeue(), Is.EqualTo("5"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("+"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("3"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("*"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("2"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("/"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("("));
           Assert.That(tokens.Dequeue(), Is.EqualTo("1"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("-"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("7"));
           Assert.That(tokens.Dequeue(), Is.EqualTo(")"));
        }

        [Test]
        public void Tokenize_LargeNumbers()
        {
           string expression = "123456789+987654321";
           ExpressionTree tree = new ExpressionTree(expression);

           Queue<string> tokens = tree.Tokenize(expression);

           Assert.That(tokens.Dequeue(), Is.EqualTo("123456789"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("+"));
           Assert.That(tokens.Dequeue(), Is.EqualTo("987654321"));
        }
    }
}
