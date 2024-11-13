using SpreadsheetEngine.Commands;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SpreadsheetEngine
{
    internal class SaveAndLoadTests
    {
        [Test]
        public void TestSave_SpreadsheetWithModifiedCells()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cellA1 = sheet.GetCell(0, 0);
            cellA1.Text = "Hello";
            cellA1.BGColor = 0xFFFF0000;

            var cellB2 = sheet.GetCell(1, 1);
            cellB2.Text = "=A1+5";

            using (MemoryStream stream = new MemoryStream())
            {
                sheet.Save(stream);
                stream.Position = 0;
                string xmlOutput = new StreamReader(stream).ReadToEnd();

                Assert.IsTrue(xmlOutput.Contains("name=\"A1\""));
                Assert.IsTrue(xmlOutput.Contains("<text>Hello</text>"));
                Assert.IsTrue(xmlOutput.Contains("<bgcolor>FFFF0000</bgcolor>"));

                Assert.IsTrue(xmlOutput.Contains("name=\"B2\""));
                Assert.IsTrue(xmlOutput.Contains("<text>=A1+5</text>"));

                Assert.IsFalse(xmlOutput.Contains("name=\"C3\""));
            }
        }

        [Test]
        public void TestLoad_TrashAttributes()
        {
            string xmlInput = @"
            <spreadsheet unusedattr=""abc"">
                <cell name=""A1"" extrainfo=""123"">
                    <bgcolor>FF00FF00</bgcolor>
                    <text>=B1+10</text>
                    <trash1>Ignore me</trash1>
                </cell>
                <cell name=""B1"">
                    <text>5</text>
                    <bgcolor>FF0000FF</bgcolor>
                    <trash>Data</trash>
                </cell>
            </spreadsheet>";

            Spreadsheet sheet = new Spreadsheet(5, 5);
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlInput)))
            {
                sheet.Load(stream);
            }

            var cellA1 = sheet.GetCell(0, 0);
            Assert.That(cellA1.Text, Is.EqualTo("=B1+10"));
            Assert.That(cellA1.BGColor, Is.EqualTo(0xFF00FF00));
            Assert.That(cellA1.Value, Is.EqualTo("15"));

            var cellB1 = sheet.GetCell(0, 1);
            Assert.That(cellB1.Text, Is.EqualTo("5"));
            Assert.That(cellB1.Value, Is.EqualTo("5"));
            Assert.That(cellB1.BGColor, Is.EqualTo(0xFF0000FF));
        }

        [Test]
        public void TestSaveAndLoad_RetainData()
        {
            Spreadsheet originalSheet = new Spreadsheet(5, 5);
            var cellA1 = originalSheet.GetCell(0, 0);
            cellA1.Text = "36";
            cellA1.BGColor = 0xFFFF0000;

            var cellB2 = originalSheet.GetCell(1, 1);
            cellB2.Text = "=A1";
            cellB2.BGColor = 0xFF00FF00;

            MemoryStream stream = new MemoryStream();
            originalSheet.Save(stream);
            stream.Position = 0;
            Spreadsheet loadedSheet = new Spreadsheet(5, 5);
            loadedSheet.Load(stream);

            var loadedCellA1 = loadedSheet.GetCell(0, 0);
            Assert.That(loadedCellA1.Text, Is.EqualTo(cellA1.Text));
            Assert.That(loadedCellA1.BGColor, Is.EqualTo(cellA1.BGColor));

            var loadedCellB2 = loadedSheet.GetCell(1, 1);
            Assert.That(loadedCellB2.Text, Is.EqualTo(cellB2.Text));
            Assert.That(loadedCellB2.BGColor, Is.EqualTo(cellB2.BGColor));
            Assert.That(loadedCellB2.Value, Is.EqualTo("36"));
        }

        [Test]
        public void TestLoad_Clears()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(0, 0);
            cell.Text = "Old Data";
            sheet.AddUndo(new ChangeTextCommand(cell, "Old Data"));
            Assert.IsTrue(sheet.CanUndo);

            string xmlInput = @"
            <spreadsheet>
                <cell name=""A1"">
                    <text>New Data</text>
                </cell>
            </spreadsheet>";

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlInput)))
            {
                sheet.Load(stream);
            }

            Assert.That(cell.Text, Is.EqualTo("New Data"));
            Assert.IsFalse(sheet.CanUndo);
            Assert.IsFalse(sheet.CanRedo);
        }

        [Test]
        public void TestSaveAndLoad_EmptySpreadsheet()
        {
            Spreadsheet originalSheet = new Spreadsheet(5, 5);

            // Save the empty spreadsheet
            MemoryStream stream = new MemoryStream();
            originalSheet.Save(stream);
            stream.Position = 0;

            // Load into a new spreadsheet
            Spreadsheet loadedSheet = new Spreadsheet(5, 5);
            loadedSheet.Load(stream);

            // Verify that all cells are default
            for (int i = 0; i < originalSheet.RowCount; i++)
            {
                for (int j = 0; j < originalSheet.ColumnCount; j++)
                {
                    var originalCell = originalSheet.GetCell(i, j);
                    var loadedCell = loadedSheet.GetCell(i, j);

                    Assert.That(loadedCell.Text, Is.EqualTo(originalCell.Text));
                    Assert.That(loadedCell.BGColor, Is.EqualTo(originalCell.BGColor));
                    Assert.That(loadedCell.Value, Is.EqualTo(originalCell.Value));
                }
            }
        }

        [Test]
        public void TestLoad_MalformedXML()
        {
            string xmlInput = @"
                <spreadsheet>
                    <cell name=""A1"">
                        <text>Invalid Data</text
                    </cell>
                    <cell name=""B1"">
                        <text>123</text>
                </spreadsheet>";

            Spreadsheet sheet = new Spreadsheet(5, 5);

            // Expecting an exception due to malformed XML
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlInput)))
            {
                Assert.Throws<XmlException>(() => sheet.Load(stream));
            }
        }

        [Test]
        public void TestSaveAndLoad_NumericFormulas()
        {
            Spreadsheet originalSheet = new Spreadsheet(5, 5);
            var cellA1 = originalSheet.GetCell(0, 0);
            cellA1.Text = "10";
            var cellB1 = originalSheet.GetCell(0, 1);
            cellB1.Text = "20";
            var cellC1 = originalSheet.GetCell(0, 2);
            cellC1.Text = "=A1+B1";

            // Save the spreadsheet
            MemoryStream stream = new MemoryStream();
            originalSheet.Save(stream);
            stream.Position = 0;

            // Load into a new spreadsheet
            Spreadsheet loadedSheet = new Spreadsheet(5, 5);
            loadedSheet.Load(stream);

            var loadedCellC1 = loadedSheet.GetCell(0, 2);
            Assert.That(loadedCellC1.Text, Is.EqualTo(cellC1.Text));
            Assert.That(loadedCellC1.Value, Is.EqualTo("30"));
        }
    }
}
