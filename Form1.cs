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
            FindUniques findDuplicates = new FindUniques();
            StringBuilder results = new StringBuilder();

            // Generate random number list
            List<int> randomNums = findDuplicates.RandomNumberList(10000, 0, 20000);

            // HashSet Method Output
            int hashMethodCount = findDuplicates.HashMethod(randomNums);
            results.AppendLine($"1. HashSet Method: {hashMethodCount} unique numbers");
            results.AppendLine("    Time Complexity: O(n) where n is the number of items in the list.");
            results.AppendLine("    Loops through each random number and adds it to a hashset, then returns the hashset length");

            // O(1) Space Method
            int listMethodCount = findDuplicates.ListMethod(randomNums);
            results.AppendLine($"2. O(1) Storage Method: {listMethodCount} unique numbers");
            results.AppendLine("    Time Complexity: O(n^2) where n is the number of items in the list.");
            results.AppendLine("    For each number in the list, it loops through the entire list until it finds another duplicate, if no duplicate add to unique number counter");

            // Sorted Method Output
            int sortMethodCount = findDuplicates.HashMethod(randomNums);
            results.AppendLine($"3. Sorted Method: {sortMethodCount} unique numbers");
            results.AppendLine("    Time Complexity: O(nlogn) where n is the number of items in the list.");
            results.AppendLine("    Takes the longest complexity which is the sort method, which averages around O(nlogn)");

            // Update text box to proper output
            textBox1.Text =  results.ToString();
        }

    }
}

