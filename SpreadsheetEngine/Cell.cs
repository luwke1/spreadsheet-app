// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.ComponentModel;

    public abstract class Cell : INotifyPropertyChanged
    {
        protected string text;
        protected string value;

        // Keep track of subscribed dependencies
        private readonly System.Collections.Generic.HashSet<Cell> dependencies = new System.Collections.Generic.HashSet<Cell>();

        /// <summary>
        /// Initializes a new instance of the Cell class with given row and column index.
        /// </summary>
        /// <param name="rowIndex">The row index of the cell.</param>
        /// <param name="columnIndex">The column index of the cell.</param>
        protected Cell(int rowIndex, int columnIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
            this.text = string.Empty;
            this.value = string.Empty;
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
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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