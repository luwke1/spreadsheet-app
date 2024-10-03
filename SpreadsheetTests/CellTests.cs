using System.Windows.Forms;

namespace SpreadsheetEngine
{
    public class CellTests
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
    }
}