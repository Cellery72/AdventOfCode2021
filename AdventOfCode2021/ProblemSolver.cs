﻿using AdventOfCode2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public static class ProblemSolver
    {
        /*
         *  --- Day 1: Sonar Sweep ---
                You're minding your own business on a ship at sea when the overboard alarm goes off! You rush to see if you can help. Apparently, one of the Elves tripped and accidentally sent the sleigh keys flying into the ocean!

                Before you know it, you're inside a submarine the Elves keep ready for situations like this. It's covered in Christmas lights (because of course it is), and it even has an experimental antenna that should be able to track the keys if you can boost its signal strength high enough; there's a little meter that indicates the antenna's signal strength by displaying 0-50 stars.

                Your instincts tell you that in order to save Christmas, you'll need to get all fifty stars by December 25th.

                Collect stars by solving puzzles. Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star. Good luck!

                As the submarine drops below the surface of the ocean, it automatically performs a sonar sweep of the nearby sea floor. On a small screen, the sonar sweep report (your puzzle input) appears: each line is a measurement of the sea floor depth as the sweep looks further and further away from the submarine.

                For example, suppose you had the following report:

                199
                200
                208
                210
                200
                207
                240
                269
                260
                263
                This report indicates that, scanning outward from the submarine, the sonar sweep found depths of 199, 200, 208, 210, and so on.

                The first order of business is to figure out how quickly the depth increases, just so you know what you're dealing with - you never know if the keys will get carried into deeper water by an ocean current or a fish or something.

                To do this, count the number of times a depth measurement increases from the previous measurement. (There is no measurement before the first measurement.) In the example above, the changes are as follows:

                199 (N/A - no previous measurement)
                200 (increased)
                208 (increased)
                210 (increased)
                200 (decreased)
                207 (increased)
                240 (increased)
                269 (increased)
                260 (decreased)
                263 (increased)
                In this example, there are 7 measurements that are larger than the previous measurement.

                How many measurements are larger than the previous measurement?
         */
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
        /*
         *      --- Part Two ---
                Considering every single measurement isn't as useful as you expected: there's just too much noise in the data.

                Instead, consider sums of a three-measurement sliding window. Again considering the above example:

                199  A      
                200  A B    
                208  A B C  
                210    B C D
                200  E   C D
                207  E F   D
                240  E F G  
                269    F G H
                260      G H
                263        H
                Start by comparing the first and second three-measurement windows. The measurements in the first window are marked A (199, 200, 208); their sum is 199 + 200 + 208 = 607. The second window is marked B (200, 208, 210); its sum is 618. The sum of measurements in the second window is larger than the sum of the first, so this first comparison increased.

                Your goal now is to count the number of times the sum of measurements in this sliding window increases from the previous sum. So, compare A with B, then compare B with C, then C with D, and so on. Stop when there aren't enough measurements left to create a new three-measurement sum.

                In the above example, the sum of each three-measurement window is as follows:

                A: 607 (N/A - no previous sum)
                B: 618 (increased)
                C: 618 (no change)
                D: 617 (decreased)
                E: 647 (increased)
                F: 716 (increased)
                G: 769 (increased)
                H: 792 (increased)
                In this example, there are 5 sums that are larger than the previous sum.

                Consider sums of a three-measurement sliding window. How many sums are larger than the previous sum?
         */
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

        /*
         *      --- Day 2: Dive! ---
                Now, you need to figure out how to pilot this thing.

                It seems like the submarine can take a series of commands like forward 1, down 2, or up 3:

                forward X increases the horizontal position by X units.
                down X increases the depth by X units.
                up X decreases the depth by X units.
                Note that since you're on a submarine, down and up affect your depth, and so they have the opposite result of what you might expect.

                The submarine seems to already have a planned course (your puzzle input). You should probably figure out where it's going. For example:

                forward 5
                down 5
                forward 8
                up 3
                down 8
                forward 2
                Your horizontal position and depth both start at 0. The steps above would then modify them as follows:

                forward 5 adds 5 to your horizontal position, a total of 5.
                down 5 adds 5 to your depth, resulting in a value of 5.
                forward 8 adds 8 to your horizontal position, a total of 13.
                up 3 decreases your depth by 3, resulting in a value of 2.
                down 8 adds 8 to your depth, resulting in a value of 10.
                forward 2 adds 2 to your horizontal position, a total of 15.
                After following these instructions, you would have a horizontal position of 15 and a depth of 10. (Multiplying these together produces 150.)

                Calculate the horizontal position and depth you would have after following the planned course. What do you get if you multiply your final horizontal position by your final depth?
         */
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
        /*
         *      --- Part Two ---
                Based on your calculations, the planned course doesn't seem to make any sense. You find the submarine manual and discover that the process is actually slightly more complicated.

                In addition to horizontal position and depth, you'll also need to track a third value, aim, which also starts at 0. The commands also mean something entirely different than you first thought:

                down X increases your aim by X units.
                up X decreases your aim by X units.
                forward X does two things:
                It increases your horizontal position by X units.
                It increases your depth by your aim multiplied by X.
                Again note that since you're on a submarine, down and up do the opposite of what you might expect: "down" means aiming in the positive direction.

                Now, the above example does something different:

                forward 5 adds 5 to your horizontal position, a total of 5. Because your aim is 0, your depth does not change.
                down 5 adds 5 to your aim, resulting in a value of 5.
                forward 8 adds 8 to your horizontal position, a total of 13. Because your aim is 5, your depth increases by 8*5=40.
                up 3 decreases your aim by 3, resulting in a value of 2.
                down 8 adds 8 to your aim, resulting in a value of 10.
                forward 2 adds 2 to your horizontal position, a total of 15. Because your aim is 10, your depth increases by 2*10=20 to a total of 60.
                After following these new instructions, you would have a horizontal position of 15 and a depth of 60. (Multiplying these produces 900.)

                Using this new interpretation of the commands, calculate the horizontal position and depth you would have after following the planned course. What do you get if you multiply your final horizontal position by your final depth?
         */
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

        /*
         *      --- Day 3: Binary Diagnostic ---
                The submarine has been making some odd creaking noises, so you ask it to produce a diagnostic report just in case.

                The diagnostic report (your puzzle input) consists of a list of binary numbers which, when decoded properly, can tell you many useful things about the conditions of the submarine. The first parameter to check is the power consumption.

                You need to use the binary numbers in the diagnostic report to generate two new binary numbers (called the gamma rate and the epsilon rate). The power consumption can then be found by multiplying the gamma rate by the epsilon rate.

                Each bit in the gamma rate can be determined by finding the most common bit in the corresponding position of all numbers in the diagnostic report. For example, given the following diagnostic report:

                00100
                11110
                10110
                10111
                10101
                01111
                00111
                11100
                10000
                11001
                00010
                01010
                Considering only the first bit of each number, there are five 0 bits and seven 1 bits. Since the most common bit is 1, the first bit of the gamma rate is 1.

                The most common second bit of the numbers in the diagnostic report is 0, so the second bit of the gamma rate is 0.

                The most common value of the third, fourth, and fifth bits are 1, 1, and 0, respectively, and so the final three bits of the gamma rate are 110.

                So, the gamma rate is the binary number 10110, or 22 in decimal.

                The epsilon rate is calculated in a similar way; rather than use the most common bit, the least common bit from each position is used. So, the epsilon rate is 01001, or 9 in decimal. Multiplying the gamma rate (22) by the epsilon rate (9) produces the power consumption, 198.

                Use the binary numbers in your diagnostic report to calculate the gamma rate and epsilon rate, then multiply them together. What is the power consumption of the submarine? (Be sure to represent your answer in decimal, not binary.)
         */
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
        /*
         *      --- Part Two ---
                Next, you should verify the life support rating, which can be determined by multiplying the oxygen generator rating by the CO2 scrubber rating.

                Both the oxygen generator rating and the CO2 scrubber rating are values that can be found in your diagnostic report - finding them is the tricky part. Both values are located using a similar process that involves filtering out values until only one remains. Before searching for either rating value, start with the full list of binary numbers from your diagnostic report and consider just the first bit of those numbers. Then:

                Keep only numbers selected by the bit criteria for the type of rating value for which you are searching. Discard numbers which do not match the bit criteria.
                If you only have one number left, stop; this is the rating value for which you are searching.
                Otherwise, repeat the process, considering the next bit to the right.
                The bit criteria depends on which type of rating value you want to find:

                To find oxygen generator rating, determine the most common value (0 or 1) in the current bit position, and keep only numbers with that bit in that position. If 0 and 1 are equally common, keep values with a 1 in the position being considered.
                To find CO2 scrubber rating, determine the least common value (0 or 1) in the current bit position, and keep only numbers with that bit in that position. If 0 and 1 are equally common, keep values with a 0 in the position being considered.
                For example, to determine the oxygen generator rating value using the same example diagnostic report from above:

                Start with all 12 numbers and consider only the first bit of each number. There are more 1 bits (7) than 0 bits (5), so keep only the 7 numbers with a 1 in the first position: 11110, 10110, 10111, 10101, 11100, 10000, and 11001.
                Then, consider the second bit of the 7 remaining numbers: there are more 0 bits (4) than 1 bits (3), so keep only the 4 numbers with a 0 in the second position: 10110, 10111, 10101, and 10000.
                In the third position, three of the four numbers have a 1, so keep those three: 10110, 10111, and 10101.
                In the fourth position, two of the three numbers have a 1, so keep those two: 10110 and 10111.
                In the fifth position, there are an equal number of 0 bits and 1 bits (one each). So, to find the oxygen generator rating, keep the number with a 1 in that position: 10111.
                As there is only one number left, stop; the oxygen generator rating is 10111, or 23 in decimal.
                Then, to determine the CO2 scrubber rating value from the same example above:

                Start again with all 12 numbers and consider only the first bit of each number. There are fewer 0 bits (5) than 1 bits (7), so keep only the 5 numbers with a 0 in the first position: 00100, 01111, 00111, 00010, and 01010.
                Then, consider the second bit of the 5 remaining numbers: there are fewer 1 bits (2) than 0 bits (3), so keep only the 2 numbers with a 1 in the second position: 01111 and 01010.
                In the third position, there are an equal number of 0 bits and 1 bits (one each). So, to find the CO2 scrubber rating, keep the number with a 0 in that position: 01010.
                As there is only one number left, stop; the CO2 scrubber rating is 01010, or 10 in decimal.
                Finally, to find the life support rating, multiply the oxygen generator rating (23) by the CO2 scrubber rating (10) to get 230.

                Use the binary numbers in your diagnostic report to calculate the oxygen generator rating and CO2 scrubber rating, then multiply them together. What is the life support rating of the submarine? (Be sure to represent your answer in decimal, not binary.)
         */
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

        /*
         *      --- Day 4: Giant Squid ---
                You're already almost 1.5km (almost a mile) below the surface of the ocean, already so deep that you can't see any sunlight. What you can see, however, is a giant squid that has attached itself to the outside of your submarine.

                Maybe it wants to play bingo?

                Bingo is played on a set of boards each consisting of a 5x5 grid of numbers. Numbers are chosen at random, and the chosen number is marked on all boards on which it appears. (Numbers may not appear on all boards.) If all numbers in any row or any column of a board are marked, that board wins. (Diagonals don't count.)

                The submarine has a bingo subsystem to help passengers (currently, you and the giant squid) pass the time. It automatically generates a random order in which to draw numbers and a random set of boards (your puzzle input). For example:

                7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

                22 13 17 11  0
                 8  2 23  4 24
                21  9 14 16  7
                 6 10  3 18  5
                 1 12 20 15 19

                 3 15  0  2 22
                 9 18 13 17  5
                19  8  7 25 23
                20 11 10 24  4
                14 21 16 12  6

                14 21 17 24  4
                10 16 15  9 19
                18  8 23 26 20
                22 11 13  6  5
                 2  0 12  3  7
                After the first five numbers are drawn (7, 4, 9, 5, and 11), there are no winners, but the boards are marked as follows (shown here adjacent to each other to save space):

                22 13 17 11  0         3 15  0  2 22        14 21 17 24  4
                 8  2 23  4 24         9 18 13 17  5        10 16 15  9 19
                21  9 14 16  7        19  8  7 25 23        18  8 23 26 20
                 6 10  3 18  5        20 11 10 24  4        22 11 13  6  5
                 1 12 20 15 19        14 21 16 12  6         2  0 12  3  7
                After the next six numbers are drawn (17, 23, 2, 0, 14, and 21), there are still no winners:

                22 13 17 11  0         3 15  0  2 22        14 21 17 24  4
                 8  2 23  4 24         9 18 13 17  5        10 16 15  9 19
                21  9 14 16  7        19  8  7 25 23        18  8 23 26 20
                 6 10  3 18  5        20 11 10 24  4        22 11 13  6  5
                 1 12 20 15 19        14 21 16 12  6         2  0 12  3  7
                Finally, 24 is drawn:

                22 13 17 11  0         3 15  0  2 22        14 21 17 24  4
                 8  2 23  4 24         9 18 13 17  5        10 16 15  9 19
                21  9 14 16  7        19  8  7 25 23        18  8 23 26 20
                 6 10  3 18  5        20 11 10 24  4        22 11 13  6  5
                 1 12 20 15 19        14 21 16 12  6         2  0 12  3  7
                At this point, the third board wins because it has at least one complete row or column of marked numbers (in this case, the entire top row is marked: 14 21 17 24 4).

                The score of the winning board can now be calculated. Start by finding the sum of all unmarked numbers on that board; in this case, the sum is 188. Then, multiply that sum by the number that was just called when the board won, 24, to get the final score, 188 * 24 = 4512.

                To guarantee victory against the giant squid, figure out which board will win first. What will your final score be if you choose that board?
         */
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
        /*
         *      --- Part Two ---
                On the other hand, it might be wise to try a different strategy: let the giant squid win.

                You aren't sure how many bingo boards a giant squid could play at once, so rather than waste time counting its arms, the safe thing to do is to figure out which board will win last and choose that one. That way, no matter which boards it picks, it will win for sure.

                In the above example, the second board is the last to win, which happens after 13 is eventually called and its middle column is completely marked. If you were to keep playing until this point, the second board would have a sum of unmarked numbers equal to 148 for a final score of 148 * 13 = 1924.

                Figure out which board will win last. Once it wins, what would its final score be?
         */
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

        /*
         *      --- Day 5: Hydrothermal Venture ---
                You come across a field of hydrothermal vents on the ocean floor! These vents constantly produce large, opaque clouds, so it would be best to avoid them if possible.

                They tend to form in lines; the submarine helpfully produces a list of nearby lines of vents (your puzzle input) for you to review. For example:

                0,9 -> 5,9
                8,0 -> 0,8
                9,4 -> 3,4
                2,2 -> 2,1
                7,0 -> 7,4
                6,4 -> 2,0
                0,9 -> 2,9
                3,4 -> 1,4
                0,0 -> 8,8
                5,5 -> 8,2
                Each line of vents is given as a line segment in the format x1,y1 -> x2,y2 where x1,y1 are the coordinates of one end the line segment and x2,y2 are the coordinates of the other end. These line segments include the points at both ends. In other words:

                An entry like 1,1 -> 1,3 covers points 1,1, 1,2, and 1,3.
                An entry like 9,7 -> 7,7 covers points 9,7, 8,7, and 7,7.
                For now, only consider horizontal and vertical lines: lines where either x1 = x2 or y1 = y2.

                So, the horizontal and vertical lines from the above list would produce the following diagram:

                .......1..
                ..1....1..
                ..1....1..
                .......1..
                .112111211
                ..........
                ..........
                ..........
                ..........
                222111....
                In this diagram, the top left corner is 0,0 and the bottom right corner is 9,9. Each position is shown as the number of lines which cover that point or . if no line covers that point. The top-left pair of 1s, for example, comes from 2,2 -> 2,1; the very bottom row is formed by the overlapping lines 0,9 -> 5,9 and 0,9 -> 2,9.

                To avoid the most dangerous areas, you need to determine the number of points where at least two lines overlap. In the above example, this is anywhere in the diagram with a 2 or larger - a total of 5 points.

                Consider only horizontal and vertical lines. At how many points do at least two lines overlap?
         */
        public static int Day5(List<string> input)
        {
            List<List<string>> board = new List<List<string>>();

            for (int i = 0; i < 1000; ++i) board.Add(Enumerable.Repeat(".", 1000).ToList());

            List<Tuple<Tuple<string, string>, Tuple<string, string>>> paths = new List<Tuple<Tuple<string, string>, Tuple<string, string>>>();
            foreach (string line in input)
            {
                string x1 = line.Split(" -> ")[0].Split(',')[0];
                string y1 = line.Split(" -> ")[0].Split(',')[1];
                string x2 = line.Split(" -> ")[1].Split(',')[0];
                string y2 = line.Split(" -> ")[1].Split(',')[1];

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
                for (int i = 0; i < line.Count; ++i)
                    if (line[i] != "." && (int.Parse(line[i]) >= 2))
                        count++;

            return count;
        }
        /*
         *      --- Part Two ---
                Unfortunately, considering only horizontal and vertical lines doesn't give you the full picture; you need to also consider diagonal lines.

                Because of the limits of the hydrothermal vent mapping system, the lines in your list will only ever be horizontal, vertical, or a diagonal line at exactly 45 degrees. In other words:

                An entry like 1,1 -> 3,3 covers points 1,1, 2,2, and 3,3.
                An entry like 9,7 -> 7,9 covers points 9,7, 8,8, and 7,9.
                Considering all lines from the above example would now produce the following diagram:

                1.1....11.
                .111...2..
                ..2.1.111.
                ...1.2.2..
                .112313211
                ...1.2....
                ..1...1...
                .1.....1..
                1.......1.
                222111....
                You still need to determine the number of points where at least two lines overlap. In the above example, this is still anywhere in the diagram with a 2 or larger - now a total of 12 points.

                Consider all of the lines. At how many points do at least two lines overlap?
         */
        public static int Day5Part2(List<string> input)
        {
            List<List<string>> board = new List<List<string>>();

            for (int i = 0; i < 1000; ++i) board.Add(Enumerable.Repeat(".", 1000).ToList());

            List<Tuple<Tuple<string, string>, Tuple<string, string>>> paths = new List<Tuple<Tuple<string, string>, Tuple<string, string>>>();
            foreach (string line in input)
            {
                string x1 = line.Split(" -> ")[0].Split(',')[0];
                string y1 = line.Split(" -> ")[0].Split(',')[1];
                string x2 = line.Split(" -> ")[1].Split(',')[0];
                string y2 = line.Split(" -> ")[1].Split(',')[1];

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

                    if (value == ".") value = "1";
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
                else
                {
                    int smallerX = (x1 < x2) ? x1 : x2;
                    int largerX = (x1 > x2) ? x1 : x2;
                    int smallerY = (y1 < y2) ? y1 : y2;
                    int largerY = (y1 > y2) ? y1 : y2;

                    int xDifference = largerX - smallerX;
                    int yDifference = largerY - smallerY;

                    if (xDifference == yDifference)
                    {
                        // starting point is always x1,y1 need to determine if we are counting down or up
                        bool xIncreases = (x1 < x2);
                        bool yIncreases = (y1 < y2);

                        for (int i = 0; i <= xDifference; ++i)
                        {
                            int currentYPosition = (yIncreases) ? y1 + i : y1 - i;
                            int currentXPosition = (xIncreases) ? x1 + i : x1 - i;

                            string value = board[currentYPosition][currentXPosition];
                            if (value == ".")
                                board[currentYPosition][currentXPosition] = "1";
                            else
                            {
                                int iValue = int.Parse(value);
                                iValue += 1;
                                board[currentYPosition][currentXPosition] = iValue.ToString();
                            }
                        }
                    }
                }
            }

            int count = 0;
            foreach (var line in board)
                for (int i = 0; i < line.Count; ++i)
                    if (line[i] != "." && (int.Parse(line[i]) >= 2))
                        count++;

            return count;
        }

        /*
         *      --- Day 6: Lanternfish ---
                The sea floor is getting steeper. Maybe the sleigh keys got carried this way?

                A massive school of glowing lanternfish swims past. They must spawn quickly to reach such large numbers - maybe exponentially quickly? You should model their growth rate to be sure.

                Although you know nothing about this specific species of lanternfish, you make some guesses about their attributes. Surely, each lanternfish creates a new lanternfish once every 7 days.

                However, this process isn't necessarily synchronized between every lanternfish - one lanternfish might have 2 days left until it creates another lanternfish, while another might have 4. So, you can model each fish as a single number that represents the number of days until it creates a new lanternfish.

                Furthermore, you reason, a new lanternfish would surely need slightly longer before it's capable of producing more lanternfish: two more days for its first cycle.

                So, suppose you have a lanternfish with an internal timer value of 3:

                After one day, its internal timer would become 2.
                After another day, its internal timer would become 1.
                After another day, its internal timer would become 0.
                After another day, its internal timer would reset to 6, and it would create a new lanternfish with an internal timer of 8.
                After another day, the first lanternfish would have an internal timer of 5, and the second lanternfish would have an internal timer of 7.
                A lanternfish that creates a new fish resets its timer to 6, not 7 (because 0 is included as a valid timer value). The new lanternfish starts with an internal timer of 8 and does not start counting down until the next day.

                Realizing what you're trying to do, the submarine automatically produces a list of the ages of several hundred nearby lanternfish (your puzzle input). For example, suppose you were given the following list:

                3,4,3,1,2
                This list means that the first fish has an internal timer of 3, the second fish has an internal timer of 4, and so on until the fifth fish, which has an internal timer of 2. Simulating these fish over several days would proceed as follows:

                Initial state: 3,4,3,1,2
                After  1 day:  2,3,2,0,1
                After  2 days: 1,2,1,6,0,8
                After  3 days: 0,1,0,5,6,7,8
                After  4 days: 6,0,6,4,5,6,7,8,8
                After  5 days: 5,6,5,3,4,5,6,7,7,8
                After  6 days: 4,5,4,2,3,4,5,6,6,7
                After  7 days: 3,4,3,1,2,3,4,5,5,6
                After  8 days: 2,3,2,0,1,2,3,4,4,5
                After  9 days: 1,2,1,6,0,1,2,3,3,4,8
                After 10 days: 0,1,0,5,6,0,1,2,2,3,7,8
                After 11 days: 6,0,6,4,5,6,0,1,1,2,6,7,8,8,8
                After 12 days: 5,6,5,3,4,5,6,0,0,1,5,6,7,7,7,8,8
                After 13 days: 4,5,4,2,3,4,5,6,6,0,4,5,6,6,6,7,7,8,8
                After 14 days: 3,4,3,1,2,3,4,5,5,6,3,4,5,5,5,6,6,7,7,8
                After 15 days: 2,3,2,0,1,2,3,4,4,5,2,3,4,4,4,5,5,6,6,7
                After 16 days: 1,2,1,6,0,1,2,3,3,4,1,2,3,3,3,4,4,5,5,6,8
                After 17 days: 0,1,0,5,6,0,1,2,2,3,0,1,2,2,2,3,3,4,4,5,7,8
                After 18 days: 6,0,6,4,5,6,0,1,1,2,6,0,1,1,1,2,2,3,3,4,6,7,8,8,8,8
                Each day, a 0 becomes a 6 and adds a new 8 to the end of the list, while each other number decreases by 1 if it was present at the start of the day.

                In this example, after 18 days, there are a total of 26 fish. After 80 days, there would be a total of 5934.

                Find a way to simulate lanternfish. How many lanternfish would there be after 80 days?
         */
        public static int Day6(List<string> input)
        {
            List<int> initialPool = input[0].Split(',').Select(i => int.Parse(i)).ToList();
            return Lanternfish.CalculateLanternfishReproduction(initialPool, 80);
        }
        /*
         *      --- Part Two ---
                Suppose the lanternfish live forever and have unlimited food and space. Would they take over the entire ocean?

                After 256 days in the example above, there would be a total of 26984457539 lanternfish!

                How many lanternfish would there be after 256 days?
         */
        public static long Day6Part2(List<string> input)
        {
            Dictionary<long, long> contents = new Dictionary<long, long>()
            {
                { 0,0 },
                { 1,0 },
                { 2,0 },
                { 3,0 },
                { 4,0 },
                { 5,0 },
                { 6,0 },
                { 7,0 },
                { 8,0 }
            };
            List<long> initialPool = input[0].Split(',').Select(i => long.Parse(i)).ToList();

            foreach (int i in initialPool)
                contents[i]++;

            return Lanternfish.CalculateLanternFishReproudctionRecursively(contents, 256);
        }

        /*
         *      --- Day 7: The Treachery of Whales ---
                A giant whale has decided your submarine is its next meal, and it's much faster than you are. There's nowhere to run!

                Suddenly, a swarm of crabs (each in its own tiny submarine - it's too deep for them otherwise) zooms in to rescue you! They seem to be preparing to blast a hole in the ocean floor; sensors indicate a massive underground cave system just beyond where they're aiming!

                The crab submarines all need to be aligned before they'll have enough power to blast a large enough hole for your submarine to get through. However, it doesn't look like they'll be aligned before the whale catches you! Maybe you can help?

                There's one major catch - crab submarines can only move horizontally.

                You quickly make a list of the horizontal position of each crab (your puzzle input). Crab submarines have limited fuel, so you need to find a way to make all of their horizontal positions match while requiring them to spend as little fuel as possible.

                For example, consider the following horizontal positions:

                16,1,2,0,4,2,7,1,2,14
                This means there's a crab with horizontal position 16, a crab with horizontal position 1, and so on.

                Each change of 1 step in horizontal position of a single crab costs 1 fuel. You could choose any horizontal position to align them all on, but the one that costs the least fuel is horizontal position 2:

                Move from 16 to 2: 14 fuel
                Move from 1 to 2: 1 fuel
                Move from 2 to 2: 0 fuel
                Move from 0 to 2: 2 fuel
                Move from 4 to 2: 2 fuel
                Move from 2 to 2: 0 fuel
                Move from 7 to 2: 5 fuel
                Move from 1 to 2: 1 fuel
                Move from 2 to 2: 0 fuel
                Move from 14 to 2: 12 fuel
                This costs a total of 37 fuel. This is the cheapest possible outcome; more expensive outcomes include aligning at position 1 (41 fuel), position 3 (39 fuel), or position 10 (71 fuel).

                Determine the horizontal position that the crabs can align to using the least fuel possible. How much fuel must they spend to align to that position?
         */
        public static long Day7(List<string> input)
        {
            List<int> horizontalPositions = input[0].Split(',').Select(i => int.Parse(i)).ToList();
            Dictionary<long, long> results = new Dictionary<long, long>();

            for (int i = horizontalPositions.Min(); i < horizontalPositions.Max(); ++i)
            {
                long totalFuelRequired = 0;
                Dictionary<long, long> dict = horizontalPositions.Where(h => h != i).ToList().GroupBy(k => k).ToDictionary(g => (long)g.Key, g => (long)g.Count());

                foreach (var kvp in dict)
                    totalFuelRequired += (kvp.Key > i) ? (kvp.Key - i) * kvp.Value : (i - kvp.Key) * kvp.Value;

                results.Add(i, totalFuelRequired);
            }

            return results.OrderBy(kvp => kvp.Value).First().Value;
        }
    }
}
