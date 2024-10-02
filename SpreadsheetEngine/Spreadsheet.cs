using System;
using System.ComponentModel;

namespace SpreadsheetApp
{
    class Spreadsheet
    {
        private Cell[,] cells;

        public event PropertyChangedEventHandler CellPropertyChanged;

        public Spreadsheet(int rows, int columns)
        {
            for (int i = 0; i < rows; i++) 
            {
                for (int j = 0; j < columns; j++)
                {
                    cells[i, j] = CreateCell(i, j);
                }
            
            }
        }

        private Cell CreateCell(int row, int col)
        {
            return new SpreadsheetCell(row, col);
        }

        public class SpreadsheetCell : Cell
        {
            public SpreadsheetCell(int rowIndex, int columnIndex) : base(rowIndex, columnIndex) { }
        }
    }
}
