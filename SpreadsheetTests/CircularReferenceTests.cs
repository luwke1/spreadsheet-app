using NUnit.Framework;
using System.Windows.Forms;

namespace SpreadsheetEngine
{
    public class SpreadsheetCircularReferenceTests
    {
        [Test]
        public void TestCircularReference_SelfReference()
        {
            Spreadsheet sheet = new Spreadsheet(3, 3);
            var cellA1 = sheet.GetCell(0, 0);
            cellA1.Text = "=A1";

            Assert.That(cellA1.Value, Is.EqualTo("!(circular reference)"));
        }

        [Test]
        public void TestCircularReference_DirectTwoCells()
        {
            Spreadsheet sheet = new Spreadsheet(3, 3);
            var cellA1 = sheet.GetCell(0, 0);
            var cellB1 = sheet.GetCell(0, 1);

            cellA1.Text = "=B1";
            cellB1.Text = "=A1";

            Assert.That(cellA1.Value, Is.EqualTo("0"));
            Assert.That(cellB1.Value, Is.EqualTo("!(circular reference)"));
        }

        [Test]
        public void TestCircularReference_ThreeCellChain()
        {
            Spreadsheet sheet = new Spreadsheet(3, 3);
            var cellA1 = sheet.GetCell(0, 0);
            var cellB1 = sheet.GetCell(0, 1);
            var cellC1 = sheet.GetCell(0, 2);

            cellA1.Text = "=B1";
            cellB1.Text = "=C1";
            cellC1.Text = "=A1";

            Assert.That(cellA1.Value, Is.EqualTo("0"));
            Assert.That(cellB1.Value, Is.EqualTo("0"));
            Assert.That(cellC1.Value, Is.EqualTo("!(circular reference)"));
        }

        [Test]
        public void TestCircularReference_IndirectDependency()
        {
            Spreadsheet sheet = new Spreadsheet(4, 4);
            var cellA1 = sheet.GetCell(0, 0);
            var cellB1 = sheet.GetCell(0, 1);
            var cellC1 = sheet.GetCell(0, 2);
            var cellD1 = sheet.GetCell(0, 3);

            cellA1.Text = "=B1";
            cellB1.Text = "=C1";
            cellC1.Text = "=D1";
            cellD1.Text = "=A1";

            Assert.That(cellD1.Value, Is.EqualTo("!(circular reference)"));
        }

        [Test]
        public void TestCircularReference_MultipleChains()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cellA1 = sheet.GetCell(0, 0);
            var cellB1 = sheet.GetCell(0, 1);
            var cellC1 = sheet.GetCell(0, 2);
            var cellD1 = sheet.GetCell(0, 3);
            var cellE1 = sheet.GetCell(0, 4);

            cellA1.Text = "=B1";
            cellB1.Text = "=C1";
            cellC1.Text = "=A1";

            cellD1.Text = "=E1";
            cellE1.Text = "=D1";

            Assert.That(cellA1.Value, Is.EqualTo("0"));
            Assert.That(cellB1.Value, Is.EqualTo("0"));
            Assert.That(cellC1.Value, Is.EqualTo("!(circular reference)"));

            Assert.That(cellD1.Value, Is.EqualTo("0"));
            Assert.That(cellE1.Value, Is.EqualTo("!(circular reference)"));
        }
    }
}