using System;
using System.Linq;

namespace aggregate
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(args[0]);
            double avg = Enumerable.Repeat(0, n)
                .Select(val => Console.ReadLine())
                .Select(double.Parse)
                .OrderBy(val => val)
                .Skip(n >> 3)
                .Take(n - (n >> 3 << 1))
                .Average();
            Console.WriteLine($"Average is {avg}[ms]");
        }
    }
}