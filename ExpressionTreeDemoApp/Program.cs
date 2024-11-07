namespace ExpressionTreeDemoApp
{
    using System;
    using System.Linq.Expressions;
    using SpreadsheetEngine;

    class Program
    {
        static void Main(string[] args)
        {
            ExpressionTree expTree = new ExpressionTree("A1+B1+C1", new Spreadsheet(1,1));

            while (true)
            {
                Console.WriteLine("Menu (current expression: {0})", expTree.Expression);
                Console.WriteLine("1 = Enter a new expression");
                Console.WriteLine("2 = Set a variable value");
                Console.WriteLine("3 = Evaluate the expression");
                Console.WriteLine("4 = Quit");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": // Takes a new expression and makes it the new expression tree
                        Console.WriteLine("Enter new expression: ");
                        string newExp = Console.ReadLine();
                        expTree = new ExpressionTree(newExp, new Spreadsheet(1, 1));
                        break;
                    case "2": // Allows users to set variable values
                        Console.Write("Enter variable name: ");
                        string varName = Console.ReadLine();
                        Console.Write("Enter variable value: ");
                        string varValString = Console.ReadLine();
                        if (double.TryParse(varValString, out double varValue))
                        {
                            expTree.SetVariable(varName, varValue);
                        }
                        else
                        {
                            Console.WriteLine("Invalid value");
                        }
                        break;
                    case "3": // Evaluates the tree and displays the evaluated value
                        string result = expTree.Evaluate();
                        Console.WriteLine($"The result of the expression is: {result}");
                        break;
                    case "4": // Ends the program loop and exits
                        return;
                    default: // If invalid input, just keep looping
                        break;
                }
            }
        }
    }
}
