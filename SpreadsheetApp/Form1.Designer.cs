namespace SpreadsheetApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            A = new DataGridViewTextBoxColumn();
            B = new DataGridViewTextBoxColumn();
            C = new DataGridViewTextBoxColumn();
            D = new DataGridViewTextBoxColumn();
            E = new DataGridViewTextBoxColumn();
            F = new DataGridViewTextBoxColumn();
            G = new DataGridViewTextBoxColumn();
            button1 = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveSpreadsheetToolStripMenuItem = new ToolStripMenuItem();
            loadSpreadsheetToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            cellToolStripMenuItem = new ToolStripMenuItem();
            changeBackgroundToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { A, B, C, D, E, F, G });
            dataGridView1.Location = new Point(0, 23);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(664, 303);
            dataGridView1.TabIndex = 0;
            // 
            // A
            // 
            A.HeaderText = "A";
            A.MinimumWidth = 6;
            A.Name = "A";
            A.Width = 125;
            // 
            // B
            // 
            B.HeaderText = "B";
            B.MinimumWidth = 6;
            B.Name = "B";
            B.Width = 125;
            // 
            // C
            // 
            C.HeaderText = "C";
            C.MinimumWidth = 6;
            C.Name = "C";
            C.Width = 125;
            // 
            // D
            // 
            D.HeaderText = "D";
            D.MinimumWidth = 6;
            D.Name = "D";
            D.Width = 125;
            // 
            // E
            // 
            E.HeaderText = "E";
            E.MinimumWidth = 6;
            E.Name = "E";
            E.Width = 125;
            // 
            // F
            // 
            F.HeaderText = "F";
            F.MinimumWidth = 6;
            F.Name = "F";
            F.Width = 125;
            // 
            // G
            // 
            G.HeaderText = "G";
            G.MinimumWidth = 6;
            G.Name = "G";
            G.Width = 125;
            // 
            // button1
            // 
            button1.Location = new Point(255, 326);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(150, 20);
            button1.TabIndex = 1;
            button1.Text = "Perform Demo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, cellToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(663, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveSpreadsheetToolStripMenuItem, loadSpreadsheetToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveSpreadsheetToolStripMenuItem
            // 
            saveSpreadsheetToolStripMenuItem.Name = "saveSpreadsheetToolStripMenuItem";
            saveSpreadsheetToolStripMenuItem.Size = new Size(180, 22);
            saveSpreadsheetToolStripMenuItem.Text = "Save spreadsheet";
            saveSpreadsheetToolStripMenuItem.Click += SaveSpreadsheetToolStripMenuItem_Click;
            // 
            // loadSpreadsheetToolStripMenuItem
            // 
            loadSpreadsheetToolStripMenuItem.Name = "loadSpreadsheetToolStripMenuItem";
            loadSpreadsheetToolStripMenuItem.Size = new Size(180, 22);
            loadSpreadsheetToolStripMenuItem.Text = "Load spreadsheet";
            loadSpreadsheetToolStripMenuItem.Click += LoadSpreadsheetToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(103, 22);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += UndoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(103, 22);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += RedoToolStripMenuItem_Click;
            // 
            // cellToolStripMenuItem
            // 
            cellToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeBackgroundToolStripMenuItem });
            cellToolStripMenuItem.Name = "cellToolStripMenuItem";
            cellToolStripMenuItem.Size = new Size(39, 20);
            cellToolStripMenuItem.Text = "Cell";
            // 
            // changeBackgroundToolStripMenuItem
            // 
            changeBackgroundToolStripMenuItem.Name = "changeBackgroundToolStripMenuItem";
            changeBackgroundToolStripMenuItem.Size = new Size(212, 22);
            changeBackgroundToolStripMenuItem.Text = "Change background color";
            changeBackgroundToolStripMenuItem.Click += ChangeBackgroundToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(663, 346);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn A;
        private DataGridViewTextBoxColumn B;
        private DataGridViewTextBoxColumn C;
        private DataGridViewTextBoxColumn D;
        private DataGridViewTextBoxColumn E;
        private DataGridViewTextBoxColumn F;
        private DataGridViewTextBoxColumn G;
        private Button button1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem cellToolStripMenuItem;
        private ToolStripMenuItem changeBackgroundToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem saveSpreadsheetToolStripMenuItem;
        private ToolStripMenuItem loadSpreadsheetToolStripMenuItem;
    }
}
