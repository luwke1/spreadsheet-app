using System.Windows.Forms;

namespace SpreadsheetEngine
{
    public class SpreadsheetTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LoadSpreadsheet_Boundary()
        {
            int rows = 1;
            int columns = 1;

            Spreadsheet spreadsheet = new Spreadsheet(rows, columns);

            Assert.That(spreadsheet.RowCount, Is.EqualTo(1));
            Assert.That(spreadsheet.ColumnCount, Is.EqualTo(1));
        }

        [Test]
        public void LoadSpreadsheet_Normal()
        {
            int rows = 10;
            int columns = 5;

            Spreadsheet spreadsheet = new Spreadsheet(rows, columns);

            Assert.That(spreadsheet.RowCount, Is.EqualTo(rows));
            Assert.That(spreadsheet.ColumnCount, Is.EqualTo(columns));
        }

        [Test]
        public void LoadSpreadsheet_Error()
        {
            Assert.Throws<ArgumentException>(() => new Spreadsheet(0, 5));
            Assert.Throws<ArgumentException>(() => new Spreadsheet(5, 0));
            Assert.Throws<ArgumentException>(() => new Spreadsheet(1, 27));
        }

        [Test]
        public void CheckBlankCell()
        {
            Spreadsheet spreadsheet = new Spreadsheet(3, 3);

            Cell cell = spreadsheet.GetCell(1, 1);

            Assert.IsNotNull(cell);
            Assert.That(cell.RowIndex, Is.EqualTo(1));
            Assert.That(cell.ColumnIndex, Is.EqualTo(1));
        }

        [Test]
        public void CheckProperText()
        {
            Spreadsheet spreadsheet = new Spreadsheet(3, 3);

            spreadsheet.GetCell(1, 1).Text = "TEST";
            Cell cell = spreadsheet.GetCell(1, 1);

            Assert.IsNotNull(cell);
            Assert.That(cell.Text, Is.EqualTo("TEST"));
        }

        [Test]
        public void CheckProperValue()
        {
            Spreadsheet spreadsheet = new Spreadsheet(3, 3);

            spreadsheet.GetCell(1, 1).Text = "(1,1)";
            spreadsheet.GetCell(2, 2).Text = "=B2";

            Cell cell1 = spreadsheet.GetCell(1, 1);
            Cell cell2 = spreadsheet.GetCell(2, 2);

            Assert.IsNotNull(cell2);
            Assert.That(cell1.Text, Is.EqualTo("(1,1)"));
            Assert.That(cell2.Value, Is.EqualTo("(1,1)"));
        }
    }
}