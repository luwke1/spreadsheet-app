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
            List<int> randomNums = randomNumberList(10000, 0, 20000);

            // HashSet Method Output
            int hashMethodCount = duplicatesHashMethod(randomNums);
            results.AppendLine($"1. HashSet Method: {hashMethodCount} unique numbers");
            results.AppendLine("    Time Complexity: O(n) where n is the number of items in the list.");
            results.AppendLine("    Loops through each random number and adds it to a hashset, then returns the hashset length");

            // O(1) Space Method
            int listMethodCount = duplicatesListMethod(randomNums);
            results.AppendLine($"2. O(1) Storage Method: {listMethodCount} unique numbers");
            results.AppendLine("    Time Complexity: O(n^2) where n is the number of items in the list.");
            results.AppendLine("    For each number in the list, it loops through the entire list until it finds another duplicate, if no duplicate add to unique number counter");

            // Sorted Method Output
            int sortMethodCount = duplicatesHashMethod(randomNums);
            results.AppendLine($"3. Sorted Method: {sortMethodCount} unique numbers");
            results.AppendLine("    Time Complexity: O(nlogn) where n is the number of items in the list.");
            results.AppendLine("    Takes the longest complexity which is the sort method, which averages around O(nlogn)");

            // Update text box to proper output
            textBox1.Text =  results.ToString();
        }

        // Generates a list of random numbers with amount of numbers, minValue, and maxValue
        public static List<int> randomNumberList(int amount, int minValue, int maxValue)
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
        private int duplicatesHashMethod(List<int> numbers)
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
        private int duplicatesListMethod(List<int> numbers)
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

        // Returns the amount of duplicate numbers using a sorted list
        private int duplicatesSortMethod(List<int> numbers)
        {
            int dupeCount = 0;
            numbers.Sort();

            // Loops through if previous element is the same, add to duplicate counter
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] == numbers[i - 1])
                {
                    dupeCount++;
                }
            }

            // Returns the difference between total number count and duplicate count
            return numbers.Count - dupeCount;
        }
    }
}

