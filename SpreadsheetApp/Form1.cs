namespace SpreadsheetApp
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the Form1 class
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitializeDataGrid();
        }

        /// <summary>
        /// Initializes the DataGridView by calling loadCells() to populate the rows.
        /// </summary>
        private void InitializeDataGrid()
        {
            loadCells(dataGridView1, 50);
        }

        /// <summary>
        /// Populates the specified dataGridView with a number of rows and sets the row headers.
        /// </summary>
        /// <param name="dataGrid">The dataGridView object to be populated.</param>
        /// <param name="amountOfCells">The number of rows to add.</param>
        public void loadCells(DataGridView dataGrid, int amountOfCells)
        {
            dataGrid.Rows.Clear();

            // Make sure datGrid has a column
            if (dataGrid.Columns.Count == 0)
            {
                dataGrid.Columns.Add("A", "A");
            }

            // Add the amount of rows specified with amountOfCells param
            for (int i = 1; i < amountOfCells + 1; i++)
            {
                dataGrid.Rows.Add("");
                dataGrid.Rows[i - 1].HeaderCell.Value = i.ToString();
            }
        }
    }
}
