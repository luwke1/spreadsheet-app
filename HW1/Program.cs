// See https://aka.ms/new-console-template for more information
class Program
{ 
    static void Main(string[] args)
    {
        // Step 1
        Console.WriteLine("Enter numbers: \n");
        string input = Console.ReadLine();
        string[] nums = input.Split(" ");
        
        // Step 2
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
        Console.WriteLine("Sorted BST: ");
        bst.InOrderTraversal();
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
}

