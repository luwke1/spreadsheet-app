// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Represents a spreadsheet with rows and columns of cells.
    /// </summary>
    public class Spreadsheet
    {
        private Cell[,] cells;
        private int rowCount;
        private int columnCount;

        // Private undo and redo stacks
        private Stack<ICommand> undoStack = new Stack<ICommand>();
        private Stack<ICommand> redoStack = new Stack<ICommand>();

        public int RowCount { get { return this.rowCount; } }

        public int ColumnCount { get { return this.columnCount; } }

        public bool CanUndo => this.undoStack.Count > 0;
        public bool CanRedo => this.redoStack.Count > 0;

        public event PropertyChangedEventHandler CellPropertyChanged;

        /// <summary>
        /// Initializes Spreadsheet class with specified dimensions.
        /// </summary>
        /// <param name="rows">Number of rows in the spreadsheet.</param>
        /// <param name="columns">Number of columns in the spreadsheet.</param>
        public Spreadsheet(int rows, int columns)
        {
            if (rows <= 0)
                throw new ArgumentException("Number of rows must be positive.");
            if (columns <= 0 || columns >26)
                throw new ArgumentException("Number of columns must be positive and under 27.");

            this.rowCount = rows;
            this.columnCount = columns;
            this.cells = new Cell[rows, columns];

            // Loops to add cells and track their changes
            for (int i = 0; i < rows; i++) 
            {
                for (int j = 0; j < columns; j++)
                {
                    this.cells[i, j] = this.CreateCell(i, j);
                    this.cells[i,j].PropertyChanged += this.OnCellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Handles PropertyChanged events from a cell.
        /// </summary>
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (cell == null) { return; }

            this.UpdateCellValue(cell);

            this.CellPropertyChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Updates the Value of a cell based on the text inside.
        /// Manages subscriptions to referenced cells' PropertyChanged events.
        /// </summary>
        /// <param name="cell">The cell to update.</param>
        private void UpdateCellValue(Cell cell)
        {
            // Unsubscribe from all previously referenced cells
            cell.UnsubscribeFromDependencies();

            if (cell.Text.StartsWith("="))
            {
                // Extract the formula part
                string formula = cell.Text.Substring(1).Trim();

                // Create an expression tree (assumed to parse the formula)
                ExpressionTree expTree = new ExpressionTree(formula, this);

                // Subscribe to all referenced cells
                foreach (string cellName in expTree.Variables)
                {
                    Cell referencedCell = this.GetCellByName(cellName);
                    if (referencedCell != null)
                    {
                        cell.AddDependency(referencedCell);
                    }
                    else
                    {
                        cell.Value = "ERROR";
                        return;
                    }
                }

                // Evaluate the expression tree to update the cell's value
                cell.Value = expTree.Evaluate();
            }
            else
            {
                // If not a formula, set the value directly
                cell.Value = cell.Text;
            }
        }

        // Public method to add an undo action
        public void AddUndo(ICommand command)
        {
            if (command != null)
            {
                this.undoStack.Push(command);
                this.redoStack.Clear(); // Clear redo stack when a new action is performed
            }
        }

        // Public methods to perform undo and redo
        public void Undo()
        {
            if (this.undoStack.Count > 0)
            {
                ICommand command = this.undoStack.Pop();
                command.Undo();
                this.redoStack.Push(command);
            }
        }

        public void Redo()
        {
            if (this.redoStack.Count > 0)
            {
                ICommand command = this.redoStack.Pop();
                command.Execute();
                this.undoStack.Push(command);
            }
        }

        /// <summary>
        /// Parses a formula and returns the evaluated formula
        /// </summary>
        /// <param name="formula">Hte formula to be evaluated.</param>
        /// <returns>The evaluated value returned from the formula.</returns>
        public string? GetCellValue(string formula)
        {
            Console.WriteLine(formula);
            // Checks if it is a valid formula
            if (formula.Length < 2 || !char.IsLetter(formula[0]) || !char.IsDigit(formula[1]))
            {
                return "ERROR";
            }

            // Gets col and row index, zero based indexing
            int col = formula[0] - 'A';
            int row = Convert.ToInt32(formula.Substring(1).Trim()) - 1;

            // Gets the cell and index location
            Cell cell = this.GetCell(row, col);
            if (cell == null)
            {
                return "ERROR";
            }

            // Returns the evaluated cells value as a string
            string cellVal = cell.Value.ToString();
            return cellVal;
        }

        /// <summary>
        /// Parses a cell name and returns the associated cell
        /// </summary>
        /// <param name="name">The cell name to be evaluated.</param>
        /// <returns>The cell that is associated with the indexed name.</returns>
        public Cell? GetCellByName(string name)
        {
            Console.WriteLine(name);
            // Checks if it is a valid formula
            if (name.Length < 2 || !char.IsLetter(name[0]) || !char.IsDigit(name[1]))
            {
                return null;
            }

            // Gets col and row index, zero based indexing
            int col = name[0] - 'A';
            int row = Convert.ToInt32(name.Substring(1).Trim()) - 1;

            // Gets the cell and index location
            Cell cell = this.GetCell(row, col);
            if (cell == null)
            {
                return null;
            }
            return cell;
        }

        /// <summary>
        /// Retrieves the cell at the specified row and column.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns>The cell at the specified index.</returns>
        public Cell GetCell(int row, int column)
        {
            if (row < 0 || row >= this.rowCount || column < 0 || column >= this.columnCount)
            {
                return null;
            }

            return this.cells[row, column];
        }

        private Cell CreateCell(int row, int col)
        {
            return new SpreadsheetCell(row, col);
        }

        private class SpreadsheetCell : Cell
        {
            public SpreadsheetCell(int rowIndex, int columnIndex) : base(rowIndex, columnIndex) { }
        }
    }
}
