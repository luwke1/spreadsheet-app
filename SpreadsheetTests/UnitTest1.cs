using System.Windows.Forms;

namespace SpreadsheetApp
{
    public class Tests
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

            form.loadCells(dataGridView, numberOfCells);

            Assert.AreEqual(numberOfCells, dataGridView.Rows.Count-1, "Amount of rows not added properly");
        }

        // Boundary Case: Ensure no rows are added when amountOfCells is zero.
        [Test]
        public void LoadCells_Zero()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            form.loadCells(dataGridView, 0);

            Assert.AreEqual(0, dataGridView.Rows.Count, "No rows should be added when amountOfCells is 0.");
        }

        // Upper Boundary Case: Test with a large number of rows
        [Test]
        public void LoadCells_Large()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            int numberOfCells = 1000;

            form.loadCells(dataGridView, numberOfCells);

            Assert.AreEqual(numberOfCells, dataGridView.Rows.Count - 1, "Incorrect number of rows added for a large number of cells.");
        }

        // Error Case: Ensure that no rows are added and handle negative values.
        [Test]
        public void LoadCells_Negeative()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            form.loadCells(dataGridView, -5);

            Assert.AreEqual(0, dataGridView.Rows.Count - 1, "No rows should be added when amountOfCells is negative.");
        }
    }
}