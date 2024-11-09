// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class with specific row and column indices.
    /// </summary>
    /// <param name="rowIndex">The row index of the cell.</param>
    /// <param name="columnIndex">The column index of the cell.</param>
    public abstract class Cell(int rowIndex, int columnIndex) : INotifyPropertyChanged
    {
        // Keep track of subscribed dependencies
        private readonly System.Collections.Generic.HashSet<Cell> dependencies = new System.Collections.Generic.HashSet<Cell>();

        private string text = string.Empty;
        private string value = string.Empty;
        private uint bGColor = 0xFFFFFFFF;

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the row index of the cell.
        /// </summary>
        public int RowIndex { get; } = rowIndex;

        /// <summary>
        /// Gets the column index of the cell.
        /// </summary>
        public int ColumnIndex { get; } = columnIndex;

        /// <summary>
        /// Gets or sets the background color of a cell.
        /// </summary>
        public uint BGColor
        {
            get => this.bGColor;
            set
            {
                this.bGColor = value;
                this.OnPropertyChanged("BGColor");
            }
        }

        /// <summary>
        /// Gets or sets the text of the cell.
        /// </summary>
        public string Text
        {
            get => this.text;
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.OnPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets the evaluated value of the cell.
        /// </summary>
        public string Value
        {
            get => this.value;
            internal set // Making setter only possible internally
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.OnPropertyChanged("Value");
                }
            }
        }

        /// <summary>
        /// Adds a cell to the dependencies.
        /// </summary>
        /// <param name="cell">The cell to add as a dependency.</param>
        public void AddDependency(Cell cell)
        {
            if (cell != null && !this.dependencies.Contains(cell))
            {
                this.dependencies.Add(cell);
                cell.PropertyChanged += this.OnReferencedCellChanged;
            }
        }

        /// <summary>
        /// Unsubscribes from all previously referenced cells.
        /// </summary>
        public void UnsubscribeFromDependencies()
        {
            foreach (var dep in this.dependencies)
            {
                dep.PropertyChanged -= this.OnReferencedCellChanged;
            }

            this.dependencies.Clear();
        }

        /// <summary>
        /// Method to raise the PropertyEvent event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Event handler for changes in referenced cells.
        /// Triggers reevaluation of this cell.
        /// </summary>
        private void OnReferencedCellChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                // Trigger reevaluation by raising PropertyChanged on this cell's Text
                // This will cause the Spreadsheet's OnCellPropertyChanged to be invoked
                // which in turn calls UpdateCellValue
                this.OnPropertyChanged("Text");
            }
        }
    }
}