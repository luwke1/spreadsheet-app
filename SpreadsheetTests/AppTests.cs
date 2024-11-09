// <copyright file="AppTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetApp
{
    using System.Windows.Forms;

    public class AppTests
    {
        [Test]
        public void LoadCells50()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            int numberOfCells = 50;

            form.LoadCells(dataGridView, numberOfCells);

            Assert.That(dataGridView.Rows.Count-1, Is.EqualTo(numberOfCells), "Amount of rows not added properly");
        }

        // Boundary Case: Ensure no rows are added when amountOfCells is zero.
        [Test]
        public void LoadCellsZero()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            form.LoadCells(dataGridView, 0);

            Assert.That(dataGridView.Rows.Count-1, Is.EqualTo(0), "No rows should be added when amountOfCells is 0.");
        }

        // Upper Boundary Case: Test with a large number of rows
        [Test]
        public void LoadCellsLarge()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            int numberOfCells = 1000;

            form.LoadCells(dataGridView, numberOfCells);

            Assert.That(dataGridView.Rows.Count - 1, Is.EqualTo(numberOfCells), "Incorrect number of rows added for a large number of cells.");
        }

        // Error Case: Ensure that no rows are added and handle negative values.
        [Test]
        public void LoadCellsNegative()
        {
            Form1 form = new Form1();
            DataGridView dataGridView = new DataGridView();

            form.LoadCells(dataGridView, -5);

            Assert.That(dataGridView.Rows.Count - 1, Is.EqualTo(0), "No rows should be added when amountOfCells is negative.");
        }
    }
}