using System.Collections.Generic;

namespace SpreadsheetEngine
{
    public class ChangeBackgroundColorCommand : ICommand
    {
        private readonly List<Cell> cells;
        private readonly uint newColor;
        private readonly Dictionary<Cell, uint> oldColors;

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
