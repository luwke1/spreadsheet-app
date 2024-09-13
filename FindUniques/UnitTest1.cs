namespace FindUniques
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HashMethodTest()
        {
            List<int> testList1 = [1, 2, 3, 4, 5];
            List<int> testList2 = [1, 1, 2, 2, 3];
            List<int> testList3 = [int.MaxValue, int.MinValue, 0];


            WinFormsApp1.FindUniques findUniques = new WinFormsApp1.FindUniques();

            int result1 = findUniques.HashMethod(testList1);
            int result2 = findUniques.HashMethod(testList2);
            int result3 = findUniques.HashMethod(testList3);


            Assert.That(result1, Is.EqualTo(5));
            Assert.That(result2, Is.EqualTo(3));
            Assert.That(result3, Is.EqualTo(3));
        }

        [Test]
        public void ListMethodTest()
        {
            List<int> testList1 = [1, 2, 3, 4, 5];
            List<int> testList2 = [1, 1, 2, 2, 3];
            List<int> testList3 = [int.MaxValue, int.MinValue, 0];


            WinFormsApp1.FindUniques findUniques = new WinFormsApp1.FindUniques();

            int result1 = findUniques.ListMethod(testList1);
            int result2 = findUniques.ListMethod(testList2);
            int result3 = findUniques.ListMethod(testList3);


            Assert.That(result1, Is.EqualTo(5));
            Assert.That(result2, Is.EqualTo(3));
            Assert.That(result3, Is.EqualTo(3));
        }

        [Test]
        public void SortMethodTest()
        {
            List<int> testList1 = [1, 2, 3, 4, 5];
            List<int> testList2 = [1, 1, 2, 2, 3];
            List<int> testList3 = [int.MaxValue, int.MinValue, 0];


            WinFormsApp1.FindUniques findUniques = new WinFormsApp1.FindUniques();

            int result1 = findUniques.SortMethod(testList1);
            int result2 = findUniques.SortMethod(testList2);
            int result3 = findUniques.SortMethod(testList3);


            Assert.That(result1, Is.EqualTo(5));
            Assert.That(result2, Is.EqualTo(3));
            Assert.That(result3, Is.EqualTo(3));
        }
    }
}