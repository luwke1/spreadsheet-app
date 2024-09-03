// See https://aka.ms/new-console-template for more information
class Program
{ 
    static void Main(string[] args)
    {
        Console.WriteLine("Enter numbers: \n");
        string input = Console.ReadLine();
        string[] nums = input.Split(" ");

        // Test if properly processed
        for (int i = 0; i < nums.Length; i++)
        {
            Console.Write(nums[i] + " ");
        }
    }
}
