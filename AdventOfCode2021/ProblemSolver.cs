using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    public static class ProblemSolver
    {
        public static int Day1(List<string> input)
        {
            int totalIncreased = 0;
            for (int i = 0; i < input.Count; ++i)
            {
                if (i != 0)
                {
                    int currentValue = int.Parse(input[i]);
                    int previousValue = int.Parse(input[i - 1]);

                    if (currentValue > previousValue)
                        totalIncreased++;
                }
            }
            return totalIncreased;
        }
        public static int Day1Part2(List<string> input)
        {
            List<List<int>> groups = new List<List<int>>();
            List<int> currentGroup = new List<int>();
            int totalIncreased = 0, currentCount = 0;

            foreach (string line in input)
            {
                List<int> nextGroup = new List<int>() { int.Parse(line) };
                if (currentCount != 0)
                {
                    var oldGroups = groups.Where(f => f.Count < 3);
                    foreach (var g in oldGroups)
                        g.Add(int.Parse(line));
                }
                groups.Add(nextGroup);
                currentCount++;
            }

            for (int i = 0; i < groups.Count; ++i)
                if (i != 0 && (groups[i].Sum() > groups[i - 1].Sum()))
                    totalIncreased++;

            return totalIncreased;
        }

        public static int Day2(List<string> input)
        {
            int horizontalPos = 0, depthPos = 0;
            foreach (string line in input)
            {
                string direction = line.Split(' ')[0];
                int position = int.Parse(line.Split(' ')[1]);

                switch (direction)
                {
                    case "forward":
                        horizontalPos += position;
                        break;
                    case "down":
                        depthPos += position;
                        break;
                    case "up":
                        depthPos -= position;
                        break;
                }
            }
            return horizontalPos * depthPos;
        }
        public static int Day2Part2(List<string> input)
        {
            int horizontalPos = 0, depthPos = 0, aimPos = 0;
            foreach (string line in input)
            {
                string direction = line.Split(' ')[0];
                int position = int.Parse(line.Split(' ')[1]);

                switch (direction)
                {
                    case "forward":
                        horizontalPos += position;
                        depthPos += (aimPos * position);
                        break;
                    case "down":
                        aimPos += position;
                        break;
                    case "up":
                        aimPos -= position;
                        break;
                }
            }
            return horizontalPos * depthPos;
        }

        public static int Day3(List<string> input)
        {
            int strLength = input.First().Length;
            string gammaRate = string.Empty, epsilonRate = String.Empty;

            for (int i = 0; i < strLength; ++i)
            {
                int zeroCount = input.Count(c => c[i] == '0');
                int oneCount = input.Count(c => c[i] == '1');

                if (zeroCount > oneCount)
                {
                    gammaRate += '0';
                    epsilonRate += '1';
                }
                else if (oneCount > zeroCount)
                {
                    gammaRate += '1';
                    epsilonRate += '0';
                }
            }
            int iGammaRate = Convert.ToInt32(gammaRate, 2), iEpsilonRate = Convert.ToInt32(epsilonRate, 2);

            return iGammaRate * iEpsilonRate;
        }
        public static int Day3Part2(List<string> input)
        {
            int strLength = input.First().Length;
            int oxygenRating = 0, coRating = 0;

            List<string> tempInput = new List<string>(input);
            for (int i = 0; i < strLength; ++i)
            {
                int zeroCount = tempInput.Count(c => c[i] == '0');
                int oneCount = tempInput.Count(c => c[i] == '1');

                if (tempInput.Count!=1)
                {
                    if (zeroCount > oneCount)
                        tempInput.RemoveAll(c => c[i] == '1');
                    else if (oneCount >= zeroCount)
                        tempInput.RemoveAll(c => c[i] == '0');
                }
                else
                {
                    oxygenRating = Convert.ToInt32(tempInput.First(),2);
                    break;
                }
            }
            if (tempInput.Count == 1) oxygenRating = Convert.ToInt32(tempInput[0], 2);

            tempInput = new List<string>(input);
            for (int i = 0; i < strLength; ++i)
            {
                int zeroCount = tempInput.Count(c => c[i] == '0');
                int oneCount = tempInput.Count(c => c[i] == '1');

                if (tempInput.Count != 1)
                {
                    if (oneCount >= zeroCount)
                        tempInput.RemoveAll(c => c[i] == '1');
                    else if (zeroCount > oneCount)
                        tempInput.RemoveAll(c => c[i] == '0');
                }
                else
                {
                    coRating = Convert.ToInt32(tempInput.First(), 2);
                    break;
                }
            }

            return oxygenRating*coRating;
        }

        public static int Day4(List<string> input)
        {
            List<int> drawnNumbers = input.First().Split(',').Select(int.Parse).ToList();



            return -1;
        }
    }
}
