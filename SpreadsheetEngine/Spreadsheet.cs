using System;
using System.ComponentModel;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Represents a spreadsheet with rows and columns of cells.
    /// </summary>
    public class Spreadsheet
    {
        private Cell[,] cells;
        private int rowCount;
        private int columnCount;


        public int RowCount { get { return rowCount; } }
        public int ColumnCount { get { return columnCount; } }
        

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

            rowCount = rows;
            columnCount = columns;
            cells = new Cell[rows, columns];

            // Loops to add cells and track their changes
            for (int i = 0; i < rows; i++) 
            {
                for (int j = 0; j < columns; j++)
                {
                    cells[i, j] = CreateCell(i, j);
                    cells[i,j].PropertyChanged += OnCellPropertyChanged;
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

            UpdateCellValue(cell);

            CellPropertyChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Updates the Value of a cell based on the text inside.
        /// </summary>
        /// <param name="cell">The cell to update.</param>
        private void UpdateCellValue(Cell cell)
        {
            if (cell.Text.StartsWith("="))
            {
                // Extracts the formula part of the text
                string formula = cell.Text.Substring(1).Trim();

                // Creates an expression tree
                ExpressionTree expTree = new ExpressionTree(formula, this);

                cell.Value = expTree.Evaluate();
          
            }else
            {
                cell.Value = cell.Text;
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
                throw new ArgumentException("Invalid formula format.");
            }

            // Gets col and row index, zero based indexing
            int col = formula[0] - 'A';
            int row = Convert.ToInt32(formula.Substring(1).Trim()) - 1;

            // Gets the cell and index location
            Cell cell = GetCell(row, col);
            if (cell == null)
            {
                throw new InvalidOperationException("Cell not found."+ col + " " + row);
            }

            // Returns the evaluated cells value as a string
            string cellVal = cell.Value.ToString();
            return cellVal;
        }

        /// <summary>
        /// Retrieves the cell at the specified row and column.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns>The cell at the specified index.</returns>
        public Cell GetCell(int row, int column)
        {
            if (row < 0 || row >= rowCount || column < 0 || column >= columnCount)
            {
                return null;
            }

            return cells[row, column];
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
