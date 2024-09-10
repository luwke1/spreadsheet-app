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
            RunDistinctIntegers();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RunDistinctIntegers() // this is your method 
        {
            StringBuilder results = new StringBuilder();
            List<int> randomNums = RandomNumberList(10000, 0, 20000);


            results.AppendLine(string.Join(", ", randomNums));
            textBox1.Text =  results.ToString();
        }

        // Generates a list of random numbers with amount of numbers, minValue, and maxValue
        public static List<int> RandomNumberList(int amount, int minValue, int maxValue)
        {
            // Generates random object
            var rand = new Random();
            List<int> randomNums = new List<int>(amount);

            // Loops through amount of numbers to generate and adds a random number to list
            for (int i = 0; i < amount; i++)
            {
                randomNums.Add(rand.Next(minValue, maxValue));
            }

            // Returns final list of random numbers
            return randomNums;
        }
    }
}

