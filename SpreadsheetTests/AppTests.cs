using System.Windows.Forms;

namespace SpreadsheetApp
{
    public class AppTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LoadCells_50()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            int numberOfCells = 50;

            form.LoadCells(dataGridView, numberOfCells);

            Assert.That(dataGridView.Rows.Count-1, Is.EqualTo(numberOfCells), "Amount of rows not added properly");
        }

        // Boundary Case: Ensure no rows are added when amountOfCells is zero.
        [Test]
        public void LoadCells_Zero()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            form.LoadCells(dataGridView, 0);

            Assert.That(dataGridView.Rows.Count-1, Is.EqualTo(0), "No rows should be added when amountOfCells is 0.");
        }

        // Upper Boundary Case: Test with a large number of rows
        [Test]
        public void LoadCells_Large()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            int numberOfCells = 1000;

            form.LoadCells(dataGridView, numberOfCells);

            Assert.That(dataGridView.Rows.Count - 1, Is.EqualTo(numberOfCells), "Incorrect number of rows added for a large number of cells.");
        }

        // Error Case: Ensure that no rows are added and handle negative values.
        [Test]
        public void LoadCells_Negeative()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            form.LoadCells(dataGridView, -5);

            Assert.That(dataGridView.Rows.Count - 1, Is.EqualTo(0), "No rows should be added when amountOfCells is negative.");
        }
    }
}