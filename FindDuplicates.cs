using System;
using System.Collections.Generic;

namespace WinFormsApp1
{
    /// <summary>
    /// The class for finding duplicates in a list using 3 different methods
    /// </summary>
    public class FindDuplicates
    {

        /// <summary>
        /// Generates a list of random integers within a specified range.
        /// </summary>
        /// <param name="amount">The number of random integers to generate.</param>
        /// <param name="minValue">The minimum value for random integers.</param>
        /// <param name="maxValue">The maximum value for random integers.</param>
        /// <returns>A list of randomly generated integers.</returns>
        public List<int> RandomNumberList(int amount, int minValue, int maxValue)
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

        /// <summary>
        /// Finds the number of unique integers using a HashSet.
        /// </summary>
        /// <param name="numbers">The list of integers to check for unique items</param>
        /// <returns>The count of unique integers.</returns>
        public int HashMethod(List<int> numbers)
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

        /// <summary>
        /// Finds the number of unique integers by iterating through the list (O(1) space complexity).
        /// </summary>
        /// <param name="numbers">The list of integers to check for unique items</param>
        /// <returns>The count of unique integers.</returns>
        public int ListMethod(List<int> numbers)
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

        /// <summary>
        /// Finds the number of unique integers using a sorted list.
        /// </summary>
        /// <param name="numbers">The list of integers to check for unique items</param>
        /// <returns>The count of unique integers.</returns>
        public int SortMethod(List<int> numbers)
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