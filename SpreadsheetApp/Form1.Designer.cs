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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { A, B, C, D, E, F, G });
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(759, 435);
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
            button1.Location = new Point(291, 435);
            button1.Name = "button1";
            button1.Size = new Size(171, 27);
            button1.TabIndex = 1;
            button1.Text = "Perform Demo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(758, 461);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
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
    }
}
