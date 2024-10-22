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
        Dictionary<Cell, List<Cell>> dependencies = new Dictionary<Cell, List<Cell>>();


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

                // Set dependency list in dictionary
                List<Cell> dependencyCells = new List<Cell>();
                foreach (string cellName in expTree.Variables)
                {
                    Cell referencedCell = this.GetCellByName(cellName);

                    if (referencedCell != null)
                    {
                        dependencyCells.Add(referencedCell);
                    }
                    else
                    {
                        cell.Value = "ERROR";
                        return;
                    }
                }

                // Update dependencies for the current cell
                AddDependencies(cell, dependencyCells);

                cell.Value = expTree.Evaluate();
          
            }else
            {
                cell.Value = cell.Text;
            }
        }

        /// <summary>
        /// Resets and  then adds the current dependent cells that a formula relies on.
        /// </summary>
        /// <param name="mainCell">The cell containing the formula.</param>
        /// <param name="dependencyCells">List of cells the formula depends on.</param>
        private void AddDependencies(Cell mainCell, List<Cell> dependencyCells)
        {
            // Remove the current dependencies if they exist
            if (dependencies.ContainsKey(mainCell))
            {
                dependencies[mainCell].ForEach(depCell => depCell.PropertyChanged -= OnDependentCellChanged);
                dependencies[mainCell].Clear();
            }
            else
            {
                dependencies[mainCell] = new List<Cell>();
            }

            // Add new dependencies
            foreach (var depCell in dependencyCells)
            {
                // Subscribe to the PropertyChanged event of the dependent cell
                depCell.PropertyChanged += OnDependentCellChanged;
                dependencies[mainCell].Add(depCell);
            }
        }

        /// <summary>
        /// Recalculates any formulas that depend on the changed cell.
        /// </summary>
        private void OnDependentCellChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell dependentCell = sender as Cell;
            if (dependentCell != null)
            {
                // Find the cells that depend on it
                foreach (var entry in dependencies)
                {
                    if (entry.Value.Contains(dependentCell))
                    {
                        // Reevaluate the formula for the cell
                        UpdateCellValue(entry.Key);
                    }
                }
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
            Cell cell = GetCell(row, col);
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
            Cell cell = GetCell(row, col);
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
