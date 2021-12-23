using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Models
{
    public class BingoCard
    {
        public List<int> B { get; set; }
        public List<int> I { get; set; }
        public List<int> N { get; set; }
        public List<int> G { get; set; }
        public List<int> O { get; set; }
        public List<int> DrawnNumbers
        {
            get
            {
                return _DrawnNumbers;
            }
            set
            {
                _DrawnNumbers = value;
            }
        }
        public int NumberOfDrawnNumbersBeforeWin => _NumberOfDrawnNumbersBeforeWin;

        public List<int> WinningNumbers { 
            get
            {
                List<int> winningNumbers = new List<int>();

                if (B.Any(item => DrawnNumbers.Contains(item)))
                    winningNumbers.AddRange(B.Where(n => DrawnNumbers.Contains(n)));
                if (I.Any(item => DrawnNumbers.Contains(item)))
                    winningNumbers.AddRange(I.Where(n => DrawnNumbers.Contains(n)));
                if (N.Any(item => DrawnNumbers.Contains(item)))
                    winningNumbers.AddRange(N.Where(n => DrawnNumbers.Contains(n)));
                if (G.Any(item => DrawnNumbers.Contains(item)))
                    winningNumbers.AddRange(G.Where(n => DrawnNumbers.Contains(n)));
                if (O.Any(item => DrawnNumbers.Contains(item)))
                    winningNumbers.AddRange(O.Where(n => DrawnNumbers.Contains(n)));

                return winningNumbers;
            }        
        }
        public List<int> LosingNumbers
        {
            get
            {
                List<int> losingNumbers = new List<int>();

                if (B.Any(item => !DrawnNumbers.Contains(item)))
                    losingNumbers.AddRange(B.Where(n => !DrawnNumbers.Contains(n)));
                if (I.Any(item => !DrawnNumbers.Contains(item)))
                    losingNumbers.AddRange(I.Where(n => !DrawnNumbers.Contains(n)));
                if (N.Any(item => !DrawnNumbers.Contains(item)))
                    losingNumbers.AddRange(N.Where(n => !DrawnNumbers.Contains(n)));
                if (G.Any(item => !DrawnNumbers.Contains(item)))
                    losingNumbers.AddRange(G.Where(n => !DrawnNumbers.Contains(n)));
                if (O.Any(item => !DrawnNumbers.Contains(item)))
                    losingNumbers.AddRange(O.Where(n => !DrawnNumbers.Contains(n)));

                return losingNumbers;
            }
        }
        public bool IsBingo => RowWins || ColumnWins;
        public bool CornersWin => _CornersWin();
        public bool ColumnWins => _ColumnWins();
        public bool RowWins => _RowWins();

        private bool _CornersWin()
        {
            List<int> corners = new List<int>() { B[0], O[0], B[4], O[4] };
            return corners.All(item => DrawnNumbers.Contains(item));
        }
        private bool _ColumnWins() => B.All(item => DrawnNumbers.Contains(item)) || I.All(item => DrawnNumbers.Contains(item)) || N.All(item => DrawnNumbers.Contains(item)) || G.All(item => DrawnNumbers.Contains(item)) || O.All(item => DrawnNumbers.Contains(item));
        private bool _RowWins()
        {
            int rowCount = B.Count;
            for (int i = 0; i < rowCount; ++i)
            {
                List<int> currentColumn = new List<int>()
                {
                    B[i],
                    I[i],
                    N[i],
                    G[i],
                    O[i]
                };

                if (currentColumn.All(item => DrawnNumbers.Contains(item)))
                    return true;
            }
            return false;
        }
        private List<int> _DrawnNumbers { get; set; }
        private int _NumberOfDrawnNumbersBeforeWin { get; set; }

        public BingoCard() { }
        public BingoCard(List<string> input, List<int> drawnNumbers = null)
        {
            B = new List<int>();
            I = new List<int>();
            N = new List<int>();
            G = new List<int>();
            O = new List<int>();
            DrawnNumbers = (drawnNumbers != null && drawnNumbers.Count > 0) ? new List<int>(drawnNumbers) { } : new List<int>();

            for (int i = 0; i < input.Count; ++i)
            {
                List<int> currentRow = input[i].CollapseWhitespace().Split(' ').ToList<string>().Select(int.Parse).ToList();

                B.Add(currentRow[0]);
                I.Add(currentRow[1]);
                N.Add(currentRow[2]);
                G.Add(currentRow[3]);
                O.Add(currentRow[4]);
            }
        }

        public void AddDrawnNumber(int num)
        {
            if(!IsBingo) _DrawnNumbers.Add(num);
        }
    }
}
