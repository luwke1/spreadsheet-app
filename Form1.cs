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
            results.AppendLine("    Loops through each random number and adds it to a hashset, then returns the hashset length");

            // O(1) Space Method
            int listMethodCount = DuplicatesListMethod(randomNums);
            results.AppendLine($"2. O(1) Storage Method: {listMethodCount} unique numbers");
            results.AppendLine("    Time Complexity: O(n^2) where n is the number of items in the list.");
            results.AppendLine("    For each number in the list, it loops through the entire list until it finds another duplicate, if no duplicate add to unique number counter");


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

        // returns the amount of duplicate numbers in a list using a hashset
        private int DuplicatesHashMethod(List<int> numbers)
        {
            Dictionary<int, int> hashNums = new Dictionary<int, int>();

            // Loop through each number in list, if already exists in hashset add to duplicate counter, otherwise add to set
            foreach (int num in numbers)
            {
                if (!hashNums.ContainsKey(num))
                {
                    hashNums[num] = 1;
                }
            }

            // Returns length of hashset
            return hashNums.Count;
        }

        // Returns the amount of duplicate numbers in a list using a single list
        private int DuplicatesListMethod(List<int> numbers)
        {
            // Intialize unique counter variable
            int uniqueCount = 0;

            // Loop through the list
            for (int i = 0; i < numbers.Count; i++)
            {
                bool hasDupe = false;

                // Loop through list again to find a duplicate
                for (int j = i + 1; j < numbers.Count; j++)
                {

                    // If a duplicate is found, let outer loop know this number has a duplicate
                    if (numbers[i] == numbers[j])
                    {
                        hasDupe = true;
                        break;
                    }
                }

                // If the current number does not have a duplicate, add it to the unique counter
                if (!hasDupe)
                {
                    uniqueCount++;
                }
            }

            // Return the amount of unique numbers found
            return uniqueCount;
        }
    }
}

