using System.Windows.Forms;

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
    }
}
