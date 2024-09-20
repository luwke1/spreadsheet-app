namespace TextReaderTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FibonacciTextReader_FirstNumberIsZero()
        {
            FibonacciTextReader reader = new FibonacciTextReader(1);

            string result = reader.ReadLine();

            Assert.That(result, Is.EqualTo("1: 0"));
        }

        [Test]
        public void FibonacciTextReader_SecondNumberIsOne()
        {
            FibonacciTextReader reader = new FibonacciTextReader(2);

            reader.ReadLine(); // Skip the first line
            string result = reader.ReadLine();

            Assert.That(result, Is.EqualTo("2: 1"));
        }

        [Test]
        public void FibonacciTextReader_CheckLast50()
        {
            FibonacciTextReader reader = new FibonacciTextReader(50);

            string result = reader.ReadToEnd();

            // Split the result by new lines and take the last line (the 50th line)
            string[] lines = result.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string lastLine = lines[49];

            Assert.That(lastLine, Is.EqualTo("50: 7778742049"));
        }

        [Test]
        public void FibonacciTextReader_CheckLast100()
        {
            FibonacciTextReader reader = new FibonacciTextReader(100);

            string result = reader.ReadToEnd();

            // Split the result by new lines and take the last line (the 100th line)
            string[] lines = result.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string lastLine = lines[99]; 

            Assert.That(lastLine, Is.EqualTo("100: 218922995834555169026"));
        }
    }
}