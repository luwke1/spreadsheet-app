using System.Windows.Forms;

namespace SpreadsheetEngine
{
    public class SpreadsheetTests
    {
        [Test]
        public void TestSpreadsheet_NormalCase()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            Assert.That(sheet.RowCount, Is.EqualTo(5));
            Assert.That(sheet.ColumnCount, Is.EqualTo(5));

            var cell = sheet.GetCell(1, 1);
            Assert.That(cell, Is.Not.Null);
            Assert.That(cell.Text, Is.EqualTo(""));
        }

        [Test]
        public void TestSpreadsheet_FormulaEvaluation()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cellA1 = sheet.GetCell(0, 0);
            var cellA2 = sheet.GetCell(1, 0);
            cellA1.Text = "5";
            cellA2.Text = "=A1+5";

            Assert.That(cellA2.Value, Is.EqualTo("10"));
        }

        [Test]
        public void TestSpreadsheet_SetTextInEmptyCell()
        {
            Spreadsheet sheet = new Spreadsheet(3, 3);
            var cell = sheet.GetCell(0, 0);
            cell.Text = "Hello";
            Assert.That(cell.Value, Is.EqualTo("Hello"));
        }

        [Test]
        public void TestSpreadsheet_OutOfBoundsAccess()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            Assert.That(sheet.GetCell(6, 1), Is.Null);
            Assert.That(sheet.GetCell(1, 6), Is.Null);
        }

        [Test]
        public void TestSpreadsheet_DependentCellUpdate()
        {
            Spreadsheet sheet = new Spreadsheet(3, 3);
            var cellA1 = sheet.GetCell(0, 0);
            var cellA2 = sheet.GetCell(1, 0);
            cellA1.Text = "10";
            cellA2.Text = "=A1+5";

            Assert.That(cellA2.Value, Is.EqualTo("15"));

            cellA1.Text = "20";
            Assert.That(cellA2.Value, Is.EqualTo("25"));
        }

        [Test]
        public void TestSpreadsheet_EmptyCellCase()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(2, 2);
            Assert.That(cell.Value, Is.EqualTo(""));
        }

        [Test]
        public void TestSpreadsheet_BadFormula()
        {
            Spreadsheet sheet = new Spreadsheet(3, 3);
            var cell = sheet.GetCell(0, 0); // A1
            cell.Text = "=A1**+523";

            Assert.That(cell.Value, Is.EqualTo("ERROR"));
        }

        [Test]
        public void TestSpreadsheet_BadReference()
        {
            Spreadsheet sheet = new Spreadsheet(3, 3);
            var cell = sheet.GetCell(0, 0);
            cell.Text = "=A9999";

            Assert.That(cell.Value, Is.EqualTo("ERROR"));
        }

        [Test]
        public void TestSpreadsheet_CircularReference()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cellA1 = sheet.GetCell(0, 0);
            var cellA2 = sheet.GetCell(1, 0);

            cellA1.Text = "=A2";
            cellA2.Text = "=A1";

            Assert.That(cellA1.Value, Is.EqualTo("ERROR"));
            Assert.That(cellA2.Value, Is.EqualTo("ERROR"));
        }

        [Test]
        public void TestSpreadsheet_DependencyChange()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cellB1 = sheet.GetCell(0, 1);
            var cellA1 = sheet.GetCell(0, 0);
            var cellA2 = sheet.GetCell(1, 0);

            cellB1.Text = "20";
            cellA1.Text = "=B1+5";
            cellA2.Text = "=A1+2";

            Assert.That(cellA1.Value, Is.EqualTo("25"));
            Assert.That(cellA2.Value, Is.EqualTo("27"));

            cellB1.Text = "30";
            Assert.That(cellA1.Value, Is.EqualTo("35"));
            Assert.That(cellA2.Value, Is.EqualTo("37"));
        }
    }
}