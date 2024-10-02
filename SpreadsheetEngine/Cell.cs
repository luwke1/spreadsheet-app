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
            text = string.Empty; 
            value = string.Empty;
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
        /// Gets or sets the text of the cell.
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                if (text == value)
                {
                    return;
                }

                text = value;
                OnPropertyChanged("Text");

                // Update the value if it's not a formula
                if (!text.StartsWith("="))
                    Value = text;
                else
                    Value = "";
            }
        }

        /// <summary>
        /// Gets the evaluated value of the cell.
        /// </summary>
        public string Value
        {
            get => value;
            protected set // Making setter only possible internally
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method to raise the PropertyEvent event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
