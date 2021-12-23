using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadAllLines("Input/Day4.txt").ToList();
            Console.WriteLine("Your answer is: " + ProblemSolver.Day4(input));
        }
    }
}
