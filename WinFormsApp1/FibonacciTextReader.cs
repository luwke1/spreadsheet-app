using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class FibonacciTextReader : TextReader
{
    private BigInteger previous = -1;
    private BigInteger current = 1;
    private int count = 0;
    private int maxLines;

    /// <summary>
    /// The constructer for creating a Fibonacci Sequence Text Reader.
    /// </summary>
    /// <param name="maxLines">The maximum number of fibonacci numbers to calculate.</param>
    public FibonacciTextReader(int maxLines)
    {
        this.maxLines = maxLines;
    }

    /// <summary>
    /// Function to create a string for the next fibonacci sequence number.
    /// </summary>
    /// <returns>
    /// A string of the current fibonacci number.
    /// </returns>
    public override string ReadLine()
    {
        BigInteger next = this.previous + this.current;
        this.previous = this.current;
        this.current = next;
        this.count++;
        return $"{this.count}: {next.ToString()}";
    }

    /// <summary>
    /// Reads all remaining lines from the FibonacciTextReader instance and concatenates them into a single string.
    /// </summary>
    /// <returns>
    /// A string that contains all the Fibonacci numbers up to the maximum number of lines specified, each on a new line.
    /// </returns>
    public override string ReadToEnd()
    {
        StringBuilder sb = new StringBuilder();
        // Loops maxLines and adds the current fibonacci number to string builder
        for (int i = 0; i < this.maxLines; i++)
        {
            sb.AppendLine(this.ReadLine());
        }

        return sb.ToString();
    }
}