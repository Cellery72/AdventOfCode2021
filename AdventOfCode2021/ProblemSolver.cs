using AdventOfCode2021.Models;
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

                if (tempInput.Count != 1)
                {
                    if (zeroCount > oneCount)
                        tempInput.RemoveAll(c => c[i] == '1');
                    else if (oneCount >= zeroCount)
                        tempInput.RemoveAll(c => c[i] == '0');
                }
                else
                {
                    oxygenRating = Convert.ToInt32(tempInput.First(), 2);
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

            return oxygenRating * coRating;
        }

        public static int Day4(List<string> input)
        {
            List<int> drawnNumbers = input.First().Split(',').Select(int.Parse).ToList();
            List<BingoCard> bingoCards = new List<BingoCard>();
            input.RemoveRange(0, 2);

            List<string> currentCardContents = new List<string>();

            for (int i = 0; i < input.Count(); ++i)
            {
                if (input[i] != string.Empty)
                    currentCardContents.Add(input[i]);
                else
                {
                    if (currentCardContents.Count != 0) bingoCards.Add(new BingoCard(currentCardContents));
                    currentCardContents = new List<string>();
                }
            }

            BingoCard winningCard = new BingoCard();
            foreach (int number in drawnNumbers)
            {
                bingoCards.Select(bc => { bc.DrawnNumbers.Add(number); return bc; }).ToList();
                if (bingoCards.Any(bc => bc.IsBingo))
                {
                    winningCard = bingoCards.FirstOrDefault(bc => bc.IsBingo);
                    return winningCard.LosingNumbers.Sum() * winningCard.DrawnNumbers.Last();
                }
            }

            return -1;
        }
        public static int Day4Part2(List<string> input)
        {
            List<int> drawnNumbers = input.First().Split(',').Select(int.Parse).ToList();
            List<BingoCard> bingoCards = new List<BingoCard>();
            input.RemoveRange(0, 2);

            List<string> currentCardContents = new List<string>();
            for (int i = 0; i < input.Count(); ++i)
            {
                if (input[i] != string.Empty)
                    currentCardContents.Add(input[i]);
                else
                {
                    if (currentCardContents.Count != 0) bingoCards.Add(new BingoCard(currentCardContents));
                    currentCardContents = new List<string>();
                }
            }

            List<BingoCard> winningCards = new List<BingoCard>();
            foreach (int number in drawnNumbers)
            {
                bingoCards.Where(bc => !bc.IsBingo).Select(bc => { bc.AddDrawnNumber(number); return bc; }).ToList();

                if (bingoCards.Any(bc => bc.IsBingo)) winningCards.AddRange(bingoCards.Where(bc => bc.IsBingo && !winningCards.Contains(bc)));
            }
            return winningCards.Last().LosingNumbers.Sum() * winningCards.Last().DrawnNumbers.Last();
        }

        public static int Day5(List<string> input)
        {
            List<List<string>> board = new List<List<string>>();

            for (int i = 0; i < 1000; ++i) board.Add(Enumerable.Repeat(".", 1000).ToList());

            List<Tuple<Tuple<string, string>, Tuple<string, string>>> paths = new List<Tuple<Tuple<string, string>, Tuple<string, string>>>();
            foreach (string line in input)
            {
                string str = line;
                string x1 = str.Split(" -> ")[0].Split(',')[0];
                string y1 = str.Split(" -> ")[0].Split(',')[1];
                string x2 = str.Split(" -> ")[1].Split(',')[0];
                string y2 = str.Split(" -> ")[1].Split(',')[1];

                Tuple<string, string> firstPair = new Tuple<string, string>(x1, y1);
                Tuple<string, string> secondPair = new Tuple<string, string>(x2, y2);

                paths.Add(new Tuple<Tuple<string, string>, Tuple<string, string>>(firstPair, secondPair));
            }

            foreach (Tuple<Tuple<string, string>, Tuple<string, string>> path in paths)
            {
                int x1 = int.Parse(path.Item1.Item1);
                int y1 = int.Parse(path.Item1.Item2);
                int x2 = int.Parse(path.Item2.Item1);
                int y2 = int.Parse(path.Item2.Item2);
                bool xSame = x1 == x2, ySame = y1 == y2;

                if (ySame && xSame)
                {
                    var value = board[y1][x1];

                    if (value == ".")
                        value = "1";
                    else
                    {
                        int iValue = int.Parse(value);
                        iValue += 1;
                        board[y1][x1] = iValue.ToString();
                    }
                }
                else if (xSame)
                {
                    int smallerY = (y1 < y2) ? y1 : y2;
                    int largerY = (y1 > y2) ? y1 : y2;

                    List<List<string>> selectedLines = board.GetRange(smallerY, largerY - smallerY);
                    for (int i = smallerY; i <= largerY; ++i)
                    {
                        var value = board[i][x1];

                        if (value == ".") board[i][x1] = "1";
                        else
                        {
                            int iValue = int.Parse(value);
                            iValue += 1;
                            board[i][x1] = iValue.ToString();
                        }
                    }
                }
                else if (ySame)
                {
                    int smallerX = (x1 < x2) ? x1 : x2;
                    int largerX = (x1 > x2) ? x1 : x2;

                    List<string> line = board[y1];
                    for (int i = smallerX; i <= largerX; ++i)
                    {
                        var value = line[i];

                        if (value == ".") board[y1][i] = "1";
                        else
                        {
                            int iValue = int.Parse(value);
                            iValue += 1;
                            board[y1][i] = iValue.ToString();
                        }
                    }
                }
            }

            int count = 0;
            foreach (var line in board)
            {
                int lineLength = line.Count;

                for (int i = 0; i < line.Count; ++i)
                    if (line[i] != "." && (int.Parse(line[i]) >= 2))
                        count++;
            }

            return count;
        }
    }
}
