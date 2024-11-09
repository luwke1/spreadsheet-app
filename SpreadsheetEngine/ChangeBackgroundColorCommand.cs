// <copyright file="ChangeBackgroundColorCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.Commands
{
    using System;
    using SpreadsheetEngine;

    /// <summary>
    /// Command to change the background color of a cell.
    /// </summary>
    public class ChangeBackgroundColorCommand : ICommand
    {
        private readonly Cell cell;
        private readonly uint newColor;
        private uint oldColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeBackgroundColorCommand"/> class.
        /// </summary>
        /// <param name="cell">The cell to modify.</param>
        /// <param name="newColor">The new background color value.</param>
        public ChangeBackgroundColorCommand(Cell cell, uint newColor)
        {
            this.cell = cell ?? throw new ArgumentNullException(nameof(cell));
            this.newColor = newColor;
            this.oldColor = this.cell.BGColor;
        }

        /// <inheritdoc/>
        public string Title => $"background color change";

        /// <inheritdoc/>
        public void Execute()
        {
            this.oldColor = this.cell.BGColor;
            this.cell.BGColor = this.newColor;
        }

        /// <inheritdoc/>
        public void Undo()
        {
            this.cell.BGColor = this.oldColor;
        }
    }
}