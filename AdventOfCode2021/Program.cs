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
            List<string> input = File.ReadAllLines("Input/Day5.txt").ToList();
            Console.WriteLine("Your answer is: " + ProblemSolver.Day5Part2(input));
            Console.ReadLine();
        }
    }
}
