using System.IO;
using System.Linq;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads and reads a file and displays it using LoadText().
        /// </summary>
        private void LoadFromFile()
        {
            // Opens a file dialog menu
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Filters files to only show files with .txt extension
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                // If file is selected and opened, then load the textbox with the contents
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        LoadText(reader);
                    }
                }
            }
        }

        /// <summary>
        /// Changes and displays the textbox on the form to the passed in stream reader.
        /// </summary>
        /// <param name="sr">A stream reader object of a specified file path</param>
        private void LoadText(TextReader sr)
        {
            textBox1.Text = sr.ReadToEnd();
        }

        /// <summary>
        /// When load a file button is clicked, it will open file dialog menu.
        /// </summary>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFromFile();
        }
    }
}

