// See https://aka.ms/new-console-template for more information
class Program
{ 
    static void Main(string[] args)
    {
        // Step 1
        // Parsing all the numbers and storing each one in a list
        Console.WriteLine("Enter numbers: \n");
        string input = Console.ReadLine();
        string[] nums = input.Split(" ");
        
        // Step 2
        // Looping through numString list and adding them to BST
        BST bst = new BST();
        foreach (string numString in nums)
        {
            int num = int.Parse(numString);
            if (num >= 0 && num <= 100)
            {
                bst.Insert(num);
            }
        }

        // Step 3
        Console.WriteLine("\nSorted BST: ");
        bst.InOrderTraversal();

        // Step 4
        int bstSize = bst.Count();
        int bstHeight = bst.Height();
        int bstMinHeight = bst.minHeight();

        Console.WriteLine($"\n\nNumber of items: {bstSize}");
        Console.WriteLine($"Number of levels: {bstHeight}");
        Console.WriteLine($"Minimum number of levels: {bstMinHeight}");
    }
}

class BST
{
    private class Node
    {
        public int value;
        public Node left, right;

        public Node(int val)
        {
            value = val;
            left = null;
            right = null;
        }
    }

    private Node root;

    public BST()
    {
        root = null;
    }

    public void Insert(int val)
    {
        root = InsertHelper(root, val);
    }

    private Node InsertHelper(Node root, int val)
    {
        if (root == null)
        {
            root = new Node(val);
            return root;
        }

        if (val < root.value)
        {
            root.left = InsertHelper(root.left, val);
        }
        else if (val > root.value)
        {
            root.right = InsertHelper(root.right, val);
        }

        return root;
    }

    public void InOrderTraversal()
    {
        InOrderHelper(root);
    }

    private void InOrderHelper(Node root)
    {
        if (root != null)
        {
            InOrderHelper(root.left);
            Console.Write(root.value + " ");
            InOrderHelper(root.right);
        }
    }

    public int Count()
    {
        return CountHelper(root);
    }

    // Iteratively goes through each node in the BST and counts it
    private int CountHelper(Node root)
    {
        if (root == null)
        {
            return 0;
        }
        return 1 + CountHelper(root.left) + CountHelper(root.right);
    }

    public int Height()
    {
        return HeightHelper(root);
    }

    // Iteratively goes through each node and picks the path with highest height and returns how high
    private int HeightHelper(Node root)
    {
        if (root == null)
        {
            return 0;
        }
        return 1 + Math.Max(HeightHelper(root.left), HeightHelper(root.right));
    }

    public int minHeight()
    {
        int bstSize = Count();
        return (int)Math.Ceiling(Math.Log2(bstSize + 1));
    }

}

