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
    }
}
