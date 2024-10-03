// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetApp
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using SpreadsheetEngine;

    /// <summary>
    /// Form1 initializer.
    /// </summary>
    public partial class Form1 : Form
    {
        private Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
        }

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes the DataGridView by calling loadCells() to populate the rows.
        /// </summary>
        private void InitializeDataGrid()
        {
            this.spreadsheet = new Spreadsheet(50, 26);
            this.LoadCells(this.dataGridView1, 50);
            this.spreadsheet.CellPropertyChanged += Spreadsheet_CellPropertyChanged;
        }

        private void Spreadsheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell? cell = sender as Cell;
            if (cell == null)
            {
                return;
            }

            if (e.PropertyName == "Value")
            {
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
            }
        }

        /// <summary>
        /// Populates the specified dataGridView with a number of rows and sets the row headers.
        /// </summary>
        /// <param name="dataGrid">The dataGridView object to be populated.</param>
        /// <param name="rows">The number of rows to add.</param>
        public void LoadCells(DataGridView dataGrid, int rows)
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            // Make sure datGrid has a column
            for (char c = 'A'; c <= 'Z'; c++)
            {
                dataGrid.Columns.Add(c.ToString(), c.ToString());
            }

            // Add the amount of rows specified with amountOfCells param
            for (int i = 1; i < rows + 1; i++)
            {
                dataGrid.Rows.Add();
                dataGrid.Rows[i - 1].HeaderCell.Value = i.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Setting random cells
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                int row = rand.Next(0, 50);
                int column = rand.Next(0, 26);
                this.spreadsheet.GetCell(row, column).Text = "Hello World!";
            }

            // Set column B cells
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet.GetCell(i, 1).Text = $"This is cell B{i + 1}";
            }

            // Set column A cells to reference column B
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet.GetCell(i, 0).Text = $"=B{i + 1}";
            }
        }
    }
}
