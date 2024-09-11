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

        private void RunDistinctIntegers()
        {
            // Initialization
            StringBuilder results = new StringBuilder();

            // Generate random number list
            List<int> randomNums = RandomNumberList(10000, 0, 20000);

            // HashSet Method Output
            int hashMethodCount = DuplicatesHashMethod(randomNums);
            results.AppendLine($"1. HashSet Method: {hashMethodCount} unique numbers");
            results.AppendLine("    Time Complexity: O(n) where n is the number of items in the list.");
            results.AppendLine("    Loops through each random number and adds it to a hashset, if num already exists then add to duplicate counter");

            // Update text box to proper output
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

        // Finds the amount of duplicate numbers in a list using a hashset
        private int DuplicatesHashMethod(List<int> numbers)
        {
            // Intialize duplicate counter and hashset
            int dupeCount = 0;
            Dictionary<int, int> hashNums = new Dictionary<int, int>();

            // Loop through each number in list, if already exists in hashset add to duplicate counter, otherwise add to set
            foreach (int num in numbers)
            {
                if (hashNums.ContainsKey(num))
                {
                    dupeCount++;
                }
                else
                {
                    hashNums[num] = 1;
                }
            }
            return dupeCount;
        }
    }
}

