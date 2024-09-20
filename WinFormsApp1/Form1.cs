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
            this.InitializeComponent();
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
                        this.LoadText(reader);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the textBox1 content into a txt file.
        /// </summary>
        private void SaveFile()
        {
            // Opens file dialog menu
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Filter files to only be txt
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                // If filename and path is determined, then create and write text to a new file
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, this.textBox1.Text);
                }
            }
        }

        /// <summary>
        /// Changes and displays the textbox on the form to the passed in stream reader.
        /// </summary>
        /// <param name="sr">A stream reader object of a specified file path.</param>
        private void LoadText(TextReader sr)
        {
            this.textBox1.Text = sr.ReadToEnd();
        }

        /// <summary>
        /// Loads a specified number of the fibonacci sequence into text box.
        /// </summary>
        /// <param name="maxNumbers"> The maximum number of Fibonacci sequence numbers to load.</param>
        private void LoadFibonacci(int maxNumbers)
        {
            using (FibonacciTextReader reader = new FibonacciTextReader(maxNumbers))
            {
                this.LoadText(reader);
            }
        }

        /// <summary>
        /// When load a file button is clicked, allows functionality to load a txt file content into form textBox1.
        /// </summary>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadFromFile();
        }

        /// <summary>
        /// When save a file option is clicked, allows functionality to save text box content as a .txt.
        /// </summary>
        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveFile();
        }

        /// <summary>
        /// When load first 50 fibonacci is clicked, displays the sequence in the text box.
        /// </summary>
        private void loadFibonacciNumbersfirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadFibonacci(50);
        }

        /// <summary>
        /// When load first 100 fibonacci is clicked, displays the sequence in the text box.
        /// </summary>
        private void loadFibonacciNumbersfirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadFibonacci(100);
        }


    }
}