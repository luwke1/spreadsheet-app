// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetApp
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using SpreadsheetEngine;
    using SpreadsheetEngine.Commands;

    /// <summary>
    /// Form1 initializer.
    /// </summary>
    public partial class Form1 : Form
    {
        private Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
        }

        /// <summary>
        /// Populates the specified dataGridView with a number of rows and sets the row headers.
        /// </summary>
        /// <param name="dataGrid">The dataGridView object to be populated.</param>
        /// <param name="rows">The number of rows to add.</param>
        public void LoadCells(DataGridView dataGrid, int rows)
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            // Make sure datGrid has a column
            for (char c = 'A'; c <= 'Z'; c++)
            {
                dataGrid.Columns.Add(c.ToString(), c.ToString());
            }

            // Add the amount of rows specified with amountOfCells param
            for (int i = 1; i < rows + 1; i++)
            {
                dataGrid.Rows.Add();
                dataGrid.Rows[i - 1].HeaderCell.Value = i.ToString();
            }
        }

        /// <summary>
        /// Initializes the DataGridView by calling loadCells() to populate the rows.
        /// </summary>
        private void InitializeDataGrid()
        {
            this.spreadsheet = new Spreadsheet(50, 26);
            this.LoadCells(this.dataGridView1, 50);

            this.dataGridView1.CellBeginEdit += this.DataGridView1_CellBeginEdit;
            this.dataGridView1.CellEndEdit += this.DataGridView1_CellEndEdit;
            this.spreadsheet.CellPropertyChanged += this.Spreadsheet_CellPropertyChanged;

            this.UpdateUndoRedoMenuItems();
        }

        /// <summary>
        /// Handles the event when a cell property changes, updating the DataGridView to reflect the new value.
        /// </summary>
        /// <param name="sender">The cell that triggered the event.</param>
        /// <param name="e">The event arguments containing the property name.</param>
        private void Spreadsheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell? cell = sender as Cell;
            if (cell == null)
            {
                return;
            }

            this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;

            if (e.PropertyName == nameof(Cell.BGColor))
            {
                // Update cell background color in the DataGridView
                var color = Color.FromArgb((int)cell.BGColor);
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = color;
            }
        }

        /// <summary>
        /// Updates the status of the undo and redo menu items based on the spreadsheet state.
        /// </summary>
        private void UpdateUndoRedoMenuItems()
        {
            this.undoToolStripMenuItem.Enabled = this.spreadsheet.CanUndo;
            this.redoToolStripMenuItem.Enabled = this.spreadsheet.CanRedo;

            this.undoToolStripMenuItem.Text = "Undo";
            this.redoToolStripMenuItem.Text = "Redo";
        }

        /// <summary>
        /// Handles the event when a cell begins editing by displaying the cell's current text in the editor.
        /// </summary>
        /// <param name="sender">The DataGridView initiating the edit.</param>
        /// <param name="e">The event arguments containing the row and column index of the cell.</param>
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;
            Cell cell = this.spreadsheet.GetCell(row, column);

            if (cell != null)
            {
                this.dataGridView1.Rows[row].Cells[column].Value = cell.Text;
            }
        }

        /// <summary>
        /// Handles the event when a cell edit ends by updating the cell's text and adding an undo command.
        /// </summary>
        /// <param name="sender">The DataGridView that ended editing.</param>
        /// <param name="e">The event arguments containing the row and column index of the cell.</param>
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;
            DataGridViewCell dataGridViewCell = this.dataGridView1.Rows[row].Cells[column];

            string newText = dataGridViewCell?.Value?.ToString() ?? string.Empty;

            // Update the corresponding cell in the spreadsheet and add an undo command
            Cell cell = this.spreadsheet.GetCell(row, column);
            if (cell != null)
            {
                if (cell.Text != newText)
                {
                    var textChangeCommand = new ChangeTextCommand(cell, newText);
                    textChangeCommand.Execute();
                    this.spreadsheet.AddUndo(textChangeCommand);
                    dataGridViewCell.Value = cell.Value;
                }
            }

            this.UpdateUndoRedoMenuItems();
        }

        /// <summary>
        /// Randomly sets text values in multiple cells for demonstration purposes.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Button1_Click(object sender, EventArgs e)
        {
            // Set random cells with sample text
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                int row = rand.Next(0, 50);
                int column = rand.Next(0, 26);
                this.spreadsheet.GetCell(row, column).Text = "Hello World!";
            }

            // Set specific text in column B cells
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet.GetCell(i, 1).Text = $"This is cell B{i + 1}";
            }

            // Reference column B cells from column A cells
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet.GetCell(i, 0).Text = $"=B{i + 1}";
            }
        }

        /// <summary>
        /// Changes the background color of selected cells in the DataGridView.
        /// </summary>
        /// <param name="sender">The menu item that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ChangeBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedCells.Count == 0)
            {
                return;
            }

            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    uint colorValue = (uint)selectedColor.ToArgb();

                    List<Cell> cellsToChange = new List<Cell>();

                    foreach (DataGridViewCell selectedCell in this.dataGridView1.SelectedCells)
                    {
                        int rowIndex = selectedCell.RowIndex;
                        int columnIndex = selectedCell.ColumnIndex;

                        var cell = this.spreadsheet.GetCell(rowIndex, columnIndex);
                        if (cell != null)
                        {
                            cellsToChange.Add(cell);
                        }
                    }

                    // Create and execute a command to change background colors, and add it to the undo stack
                    var bgColorCommand = new ChangeBackgroundColorCommand(cellsToChange, colorValue);
                    bgColorCommand.Execute();
                    this.spreadsheet.AddUndo(bgColorCommand);

                    this.UpdateUndoRedoMenuItems();
                }
            }
        }

        /// <summary>
        /// Undoes the last action performed on the spreadsheet.
        /// </summary>
        /// <param name="sender">The undo menu item.</param>
        /// <param name="e">The event arguments.</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.Undo();
            this.UpdateUndoRedoMenuItems();
        }

        /// <summary>
        /// Redoes the last undone action on the spreadsheet.
        /// </summary>
        /// <param name="sender">The redo menu item.</param>
        /// <param name="e">The event arguments.</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.Redo();
            this.UpdateUndoRedoMenuItems();
        }

        private void SaveSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                DefaultExt = "xml",
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    this.spreadsheet.Save(fs);
                }
            }
        }

        private void LoadSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                DefaultExt = "xml",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    this.spreadsheet.Load(fs);
                }

                this.dataGridView1.Refresh();
            }
        }
    }
}