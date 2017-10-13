using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var splitted = EnumerateNumber().Take(100).Split(11).ToArray();
        
            foreach (IEnumerable<int> chunk in splitted)
            {
                if (chunk.Any())
                {
                    Console.WriteLine(string.Join(", ", chunk));
                }
                else
                {
                    Console.WriteLine("empty!");
                }
            }
        }
        
        private static IEnumerable<int> EnumerateNumber()
        {
            int n = 0;
            while (true)
            {
                yield return n++;
            }
        }
    }
}