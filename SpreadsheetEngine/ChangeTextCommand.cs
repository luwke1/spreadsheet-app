// <copyright file="ChangeTextCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.Commands
{
    using System;
    using SpreadsheetEngine;

    /// <summary>
    /// Command to change the text of a cell.
    /// </summary>
    public class ChangeTextCommand : ICommand
    {
        private readonly Cell cell;
        private readonly string newText;
        private string oldText;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeTextCommand"/> class.
        /// </summary>
        /// <param name="cell">The cell to modify.</param>
        /// <param name="newText">The new text value.</param>
        public ChangeTextCommand(Cell cell, string newText)
        {
            this.cell = cell ?? throw new ArgumentNullException(nameof(cell));
            this.newText = newText;
            this.oldText = this.cell.Text;
        }

        /// <inheritdoc/>
        public string Title => "text change";

        /// <inheritdoc/>
        public void Execute()
        {
            this.oldText = this.cell.Text;
            this.cell.Text = this.newText;
        }

        /// <inheritdoc/>
        public void Undo()
        {
            this.cell.Text = this.oldText;
        }
    }
}