﻿// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Xml;

    /// <summary>
    /// Represents a spreadsheet with rows and columns of cells.
    /// </summary>
    public class Spreadsheet
    {
        private Cell[,] cells;
        private int rowCount;
        private int columnCount;

        // Private undo and redo stacks
        private Stack<ICommand> undoStack = new Stack<ICommand>();
        private Stack<ICommand> redoStack = new Stack<ICommand>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// Initializes Spreadsheet class with specified dimensions.
        /// </summary>
        /// <param name="rows">Number of rows in the spreadsheet.</param>
        /// <param name="columns">Number of columns in the spreadsheet.</param>
        public Spreadsheet(int rows, int columns)
        {
            if (rows <= 0)
            {
                throw new ArgumentException("Number of rows must be positive.");
            }

            if (columns <= 0 || columns > 26)
            {
                throw new ArgumentException("Number of columns must be positive and under 27.");
            }

            this.rowCount = rows;
            this.columnCount = columns;
            this.cells = new Cell[rows, columns];

            // Loops to add cells and track their changes
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.cells[i, j] = this.CreateCell(i, j);
                    this.cells[i, j].PropertyChanged += this.OnCellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// PropertyChanged event handler.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged;

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public int RowCount
        {
            get { return this.rowCount; }
        }

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public int ColumnCount
        {
            get { return this.columnCount; }
        }

        /// <summary>
        /// Gets a value indicating whether gets a value indicating true if you can undo, false otherwise.
        /// </summary>
        public bool CanUndo => this.undoStack.Count > 0;

        /// <summary>
        /// Gets a value indicating whether gets a value indicating true if you can redo, false otherwise.
        /// </summary>
        public bool CanRedo => this.redoStack.Count > 0;

        /// <summary>
        /// Adds the passed in command to the undo stack and clears redo.
        /// </summary>
        /// <param name="command">The command to be added to stack.</param>
        public void AddUndo(ICommand command)
        {
            if (command != null)
            {
                this.undoStack.Push(command);
                this.redoStack.Clear(); // Clear redo stack when a new action is performed
            }
        }

        /// <summary>
        /// Clears and resets the spreadsheet.
        /// </summary>
        public void ClearSpreadsheet()
        {
            // Clear existing data
            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.columnCount; j++)
                {
                    Cell cell = this.cells[i, j];
                    cell.Text = string.Empty;
                    cell.BGColor = 0xFFFFFFFF;
                }
            }
        }

        /// <summary>
        /// Saves the current spreadsheet into an XML file.
        /// </summary>
        /// <param name="stream">The file to write the spreadsheet into.</param>
        public void Save(Stream stream)
        {
            using (XmlWriter writer = XmlWriter.Create(stream))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");

                for (int i = 0; i < this.rowCount; i++)
                {
                    for (int j = 0; j < this.columnCount; j++)
                    {
                        Cell cell = this.GetCell(i, j);

                        if (cell.BGColor != 0xFFFFFFFF || !string.IsNullOrEmpty(cell.Text))
                        {
                            string cellName = this.GetCellName(cell.RowIndex, cell.ColumnIndex);

                            writer.WriteStartElement("cell");
                            writer.WriteAttributeString("name", cellName);

                            if (cell.BGColor != 0xFFFFFFFF)
                            {
                                writer.WriteElementString("bgcolor", cell.BGColor.ToString("X8"));
                            }

                            if (!string.IsNullOrEmpty(cell.Text))
                            {
                                writer.WriteElementString("text", cell.Text);
                            }

                            writer.WriteEndElement();
                        }
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Loads the file stream into the spreadsheet.
        /// </summary>
        /// <param name="stream">The file to load into the spreadsheet.</param>
        public void Load(Stream stream)
        {
            this.ClearSpreadsheet();

            this.undoStack.Clear();
            this.redoStack.Clear();

            using (XmlReader reader = XmlReader.Create(stream))
            {
                reader.MoveToContent();

                while (reader.ReadToFollowing("cell"))
                {
                    string cellName = reader.GetAttribute("name");
                    uint bgcolor = 0xFFFFFFFF;
                    string text = string.Empty;

                    if (reader.IsEmptyElement)
                    {
                        // Do nothing for empty elements
                    }
                    else
                    {
                        // Move to the first child of the cell element
                        reader.Read();

                        // Loop through all child nodes of the current cell element
                        while (reader.IsStartElement())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                switch (reader.Name)
                                {
                                    case "bgcolor":
                                        string colorStr = reader.ReadElementContentAsString();
                                        if (uint.TryParse(colorStr, System.Globalization.NumberStyles.HexNumber, null, out uint parsedColor))
                                        {
                                            bgcolor = parsedColor;
                                        }

                                        break;
                                    case "text":
                                        text = reader.ReadElementContentAsString();
                                        break;
                                    default:
                                        reader.Skip();
                                        break;
                                }
                            }
                            else
                            {
                                reader.Read();
                            }
                        }
                    }

                    Cell cell = this.GetCellByName(cellName);
                    if (cell != null)
                    {
                        cell.Text = text;
                        cell.BGColor = bgcolor;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the undo command and adds it to the redo stack.
        /// </summary>
        public void Undo()
        {
            if (this.undoStack.Count > 0)
            {
                ICommand command = this.undoStack.Pop();
                command.Undo();
                this.redoStack.Push(command);
            }
        }

        /// <summary>
        /// Gets the redo command and adds it to the undo stack.
        /// </summary>
        public void Redo()
        {
            if (this.redoStack.Count > 0)
            {
                ICommand command = this.redoStack.Pop();
                command.Execute();
                this.undoStack.Push(command);
            }
        }

        /// <summary>
        /// Parses a cell name and returns the value of the cell.
        /// </summary>
        /// <param name="name">The name of the cell to be evaluated.</param>
        /// <returns>The evaluated value returned from the formula.</returns>
        public string? GetCellValue(string name)
        {
            Console.WriteLine(name);

            // Checks if it is a valid formula
            if (name.Length < 2 || !char.IsLetter(name[0]) || !char.IsDigit(name[1]))
            {
                return "ERROR";
            }

            // Gets col and row index, zero based indexing
            int col = name[0] - 'A';
            int row = Convert.ToInt32(name.Substring(1).Trim()) - 1;

            // Gets the cell and index location
            Cell cell = this.GetCell(row, col);
            if (cell == null)
            {
                return "ERROR";
            }

            // Returns the evaluated cells value as a string
            string cellVal = cell.Value.ToString();
            return cellVal;
        }

        /// <summary>
        /// Generates the cell name from indices.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="col">The column index of the cell.</param>
        /// <returns>The name that represents thats cells location.</returns>
        public string GetCellName(int row, int col)
        {
            char columnName = (char)('A' + col);
            int rowName = row + 1;
            return $"{columnName}{rowName}";
        }

        /// <summary>
        /// Parses a cell name and returns the associated cell.
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
            Cell cell = this.GetCell(row, col);
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
            if (row < 0 || row >= this.rowCount || column < 0 || column >= this.columnCount)
            {
                return null;
            }

            return this.cells[row, column];
        }

        /// <summary>
        /// Handles PropertyChanged events from a cell.
        /// </summary>
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (cell == null)
            {
                return;
            }

            this.UpdateCellValue(cell);

            this.CellPropertyChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Updates the Value of a cell based on the text inside.
        /// Manages subscriptions to referenced cells' PropertyChanged events.
        /// </summary>
        /// <param name="cell">The cell to update.</param>
        private void UpdateCellValue(Cell cell)
        {
            // Unsubscribe from all previously referenced cells
            cell.UnsubscribeFromDependencies();

            if (cell.Text.StartsWith("="))
            {
                // Extract the formula part
                string formula = cell.Text.Substring(1).Trim();

                // Create an expression tree (assumed to parse the formula)
                ExpressionTree expTree = new ExpressionTree(formula, this);

                // Subscribe to all referenced cells
                foreach (string cellName in expTree.Variables)
                {
                    Cell referencedCell = this.GetCellByName(cellName);
                    if (referencedCell != null)
                    {
                        if (!cell.AddDependency(referencedCell))
                        {
                            cell.Value = "!(circular reference)";
                            return;
                        }
                    }
                    else
                    {
                        cell.Value = "ERROR: Invalid Reference";
                        return;
                    }
                }

                // Evaluate the expression tree to update the cell's value
                cell.Value = expTree.Evaluate();
            }
            else
            {
                // If not a formula, set the value directly
                cell.Value = cell.Text;
            }
        }

        private Cell CreateCell(int row, int col)
        {
            return new SpreadsheetCell(row, col);
        }

        private class SpreadsheetCell : Cell
        {
            public SpreadsheetCell(int rowIndex, int columnIndex)
                : base(rowIndex, columnIndex)
            {
            }
        }
    }
}
