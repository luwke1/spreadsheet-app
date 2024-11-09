using NUnit.Framework;

namespace SpreadsheetEngine
{
    using SpreadsheetEngine.Commands;

    [TestFixture]
    public class SpreadsheetCommandTests
    {
        [Test]
        public void TestCommand_UndoRedoNormalTextChange()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(0, 0);

            cell.Text = "Original";
            var command = new ChangeTextCommand(cell, "New Text");

            command.Execute();
            Assert.That(cell.Text, Is.EqualTo("New Text"));

            command.Undo();
            Assert.That(cell.Text, Is.EqualTo("Original"));
        }

        [Test]
        public void TestSpreadsheet_AddUndoRedoCommandForTextChange()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(1, 1);

            cell.Text = "Text1";
            Assert.That(cell.Text, Is.EqualTo("Text1"));

            var command = new ChangeTextCommand(cell, "Text2");
            sheet.AddUndo(command);

            command.Execute();
            Assert.That(cell.Text, Is.EqualTo("Text2"));
            Assert.IsTrue(sheet.CanUndo);

            sheet.Undo();
            Assert.That(cell.Text, Is.EqualTo("Text1"));

            sheet.Redo();
            Assert.That(cell.Text, Is.EqualTo("Text2"));
        }

        [Test]
        public void TestCommand_UndoRedoMultipleCommands()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(0, 0);

            cell.Text = "Initial";
            var command1 = new ChangeTextCommand(cell, "First Update");
            var command2 = new ChangeTextCommand(cell, "Second Update");

            command1.Execute();
            sheet.AddUndo(command1);
            command2.Execute();
            sheet.AddUndo(command2);
            Assert.That(cell.Text, Is.EqualTo("Second Update"));
            Assert.IsTrue(sheet.CanUndo);

            sheet.Undo();
            Assert.That(cell.Text, Is.EqualTo("First Update"));

            sheet.Undo();
            Assert.That(cell.Text, Is.EqualTo("Initial"));

            sheet.Redo();
            Assert.That(cell.Text, Is.EqualTo("First Update"));
            sheet.Redo();
            Assert.That(cell.Text, Is.EqualTo("Second Update"));
        }

        [Test]
        public void TestSpreadsheet_UndoRedoBoundaryCase()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(0, 0);

            Assert.IsFalse(sheet.CanUndo);

            cell.Text = "Original Text";
            var command = new ChangeTextCommand(cell, "Updated Text");
            command.Execute();
            sheet.AddUndo(command);

            Assert.IsTrue(sheet.CanUndo);
            sheet.Undo();
            Assert.That(cell.Text, Is.EqualTo("Original Text"));
            Assert.IsTrue(sheet.CanRedo);

            sheet.Undo();
            Assert.IsFalse(sheet.CanUndo);
        }

        [Test]
        public void TestSpreadsheet_RedoAfterNewCommandClearsRedoStack()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(0, 0);

            var command1 = new ChangeTextCommand(cell, "First Change");
            command1.Execute();
            sheet.AddUndo(command1);

            sheet.Undo();
            Assert.That(cell.Text, Is.EqualTo(string.Empty));
            Assert.IsTrue(sheet.CanRedo);

            var command2 = new ChangeTextCommand(cell, "New Change");
            command2.Execute();
            sheet.AddUndo(command2);
            Assert.IsFalse(sheet.CanRedo);
            Assert.That(cell.Text, Is.EqualTo("New Change"));
        }

        [Test]
        public void TestCommand_UndoRedoBackgroundColorChangeSingleCell()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(0, 0);

            // Set an initial color
            uint originalColor = 0xFFFFFF00; // Yellow
            cell.BGColor = originalColor;

            // Create and execute the background color command
            uint newColor = 0xFFFF0000; // Red
            var command = new ChangeBackgroundColorCommand(new List<Cell> { cell }, newColor);
            command.Execute();
            Assert.That(cell.BGColor, Is.EqualTo(newColor));

            // Undo the command and verify original color is restored
            command.Undo();
            Assert.That(cell.BGColor, Is.EqualTo(originalColor));
        }

        [Test]
        public void TestSpreadsheet_AddUndoRedoBackgroundColorChangeSingleCell()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(0, 0);

            // Set an initial color
            uint originalColor = 0xFFFFFF00; // Yellow
            cell.BGColor = originalColor;

            // Create, execute, and add the background color command to the undo stack
            uint newColor = 0xFFFF0000; // Red
            var command = new ChangeBackgroundColorCommand(new List<Cell> { cell }, newColor);
            sheet.AddUndo(command);
            command.Execute();
            Assert.That(cell.BGColor, Is.EqualTo(newColor));
            Assert.IsTrue(sheet.CanUndo);

            // Undo the command using spreadsheet and verify original color is restored
            sheet.Undo();
            Assert.That(cell.BGColor, Is.EqualTo(originalColor));

            // Redo the command using spreadsheet and verify new color is reapplied
            sheet.Redo();
            Assert.That(cell.BGColor, Is.EqualTo(newColor));
        }

        [Test]
        public void TestCommand_UndoRedoBackgroundColorChangeMultipleCells()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell1 = sheet.GetCell(0, 0);
            var cell2 = sheet.GetCell(1, 1);
            var cell3 = sheet.GetCell(2, 2);

            // Set initial colors
            uint initialColor1 = 0xFFFFFF00; // Yellow
            uint initialColor2 = 0xFF00FF00; // Green
            uint initialColor3 = 0xFF0000FF; // Blue

            cell1.BGColor = initialColor1;
            cell2.BGColor = initialColor2;
            cell3.BGColor = initialColor3;

            // Create and execute a command to change background color for multiple cells
            uint newColor = 0xFFFF0000; // Red
            var command = new ChangeBackgroundColorCommand(new List<Cell> { cell1, cell2, cell3 }, newColor);
            command.Execute();

            // Assert that each cell's color was changed
            Assert.That(cell1.BGColor, Is.EqualTo(newColor));
            Assert.That(cell2.BGColor, Is.EqualTo(newColor));
            Assert.That(cell3.BGColor, Is.EqualTo(newColor));

            // Undo and verify each cell reverts to its original color
            command.Undo();
            Assert.That(cell1.BGColor, Is.EqualTo(initialColor1));
            Assert.That(cell2.BGColor, Is.EqualTo(initialColor2));
            Assert.That(cell3.BGColor, Is.EqualTo(initialColor3));
        }

        [Test]
        public void TestSpreadsheet_UndoRedoBoundaryBackgroundColorChangeMultipleCells()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell1 = sheet.GetCell(0, 0);
            var cell2 = sheet.GetCell(1, 1);

            // Initial colors
            uint initialColor1 = 0xFFFFFF00; // Yellow
            uint initialColor2 = 0xFF00FF00; // Green

            cell1.BGColor = initialColor1;
            cell2.BGColor = initialColor2;

            // Create, execute, and add a color change command for multiple cells
            uint newColor = 0xFFFF0000; // Red
            var command = new ChangeBackgroundColorCommand(new List<Cell> { cell1, cell2 }, newColor);
            sheet.AddUndo(command);
            command.Execute();

            // Assert colors were changed
            Assert.That(cell1.BGColor, Is.EqualTo(newColor));
            Assert.That(cell2.BGColor, Is.EqualTo(newColor));
            Assert.IsTrue(sheet.CanUndo);

            // Undo using the spreadsheet and check colors reverted
            sheet.Undo();
            Assert.That(cell1.BGColor, Is.EqualTo(initialColor1));
            Assert.That(cell2.BGColor, Is.EqualTo(initialColor2));

            // Redo using the spreadsheet and check colors were reapplied
            sheet.Redo();
            Assert.That(cell1.BGColor, Is.EqualTo(newColor));
            Assert.That(cell2.BGColor, Is.EqualTo(newColor));
        }

        [Test]
        public void TestSpreadsheet_RedoAfterNewBackgroundColorCommandClearsRedoStack()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            var cell = sheet.GetCell(0, 0);

            // Set an initial color
            uint initialColor = 0xFFFFFF00; // Yellow
            cell.BGColor = initialColor;

            // First color change command
            uint color1 = 0xFFFF0000; // Red
            var command1 = new ChangeBackgroundColorCommand(new List<Cell> { cell }, color1);
            command1.Execute();
            sheet.AddUndo(command1);

            // Undo and verify color reverts
            sheet.Undo();
            Assert.That(cell.BGColor, Is.EqualTo(initialColor));
            Assert.IsTrue(sheet.CanRedo);

            // New color change command, should clear redo stack
            uint color2 = 0xFF0000FF; // Blue
            var command2 = new ChangeBackgroundColorCommand(new List<Cell> { cell }, color2);
            command2.Execute();
            sheet.AddUndo(command2);

            // Verify redo stack is cleared
            Assert.IsFalse(sheet.CanRedo);
            Assert.That(cell.BGColor, Is.EqualTo(color2));
        }
    }
}
