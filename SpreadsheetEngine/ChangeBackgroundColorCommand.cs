// <copyright file="ChangeBackgroundColorCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.Collections.Generic;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeBackgroundColorCommand"/> class.
    /// </summary>
    public class ChangeBackgroundColorCommand : ICommand
    {
        private readonly List<Cell> cells;
        private readonly uint newColor;
        private readonly Dictionary<Cell, uint> oldColors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeBackgroundColorCommand"/> class.
        /// </summary>
        /// <param name="cells">The list of cells to be changed.</param>
        /// <param name="newColor">The new color to change them to.</param>
        public ChangeBackgroundColorCommand(List<Cell> cells, uint newColor)
        {
            this.cells = cells;
            this.newColor = newColor;
            this.oldColors = new Dictionary<Cell, uint>();

            // Capture each cell's old color to support undo
            foreach (var cell in cells)
            {
                this.oldColors[cell] = cell.BGColor;
            }
        }

        /// <inheritdoc/>
        public string Title => "background color change";

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (var cell in this.cells)
            {
                cell.BGColor = this.newColor;
            }
        }

        /// <inheritdoc/>
        public void Undo()
        {
            foreach (var cell in this.cells)
            {
                if (this.oldColors.TryGetValue(cell, out uint oldColor))
                {
                    cell.BGColor = oldColor;
                }
            }
        }
    }
}
