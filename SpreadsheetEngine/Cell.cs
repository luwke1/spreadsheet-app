using System;
using System.ComponentModel;

namespace SpreadsheetApp
{
    public abstract class Cell : INotifyPropertyChanged
    {
        protected string text;
        protected string value;

        /// <summary>
        /// Initializes a new instance of the Cell class with given row and column index.
        /// </summary>
        /// <param name="rowIndex">The row index of the cell.</param>
        /// <param name="columnIndex">The column index of the cell.</param>
        protected Cell(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            text = string.Empty; // Initialize the text to an empty string
            value = string.Empty; // Initialize the value to an empty string
        }

        /// <summary>
        /// Gets the row index of the cell.
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Gets the column index of the cell.
        /// </summary>
        public int ColumnIndex { get; }

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
