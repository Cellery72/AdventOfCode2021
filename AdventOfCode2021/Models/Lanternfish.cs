using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Models
{
    public class Lanternfish
    {
        public int DaysRemaining { get; set; }
        public Lanternfish(int daysRemaining)
        {
            DaysRemaining = daysRemaining;
        }

        public static int CalculateLanternfishReproduction(List<int> initialPool, int xNumberOfDays)
        {
            // Calculates the total number of lanternfish after xNumberOfDays.. apparently these things reproduce quickly.
            List<Lanternfish> startingPool = new List<Lanternfish>();
            foreach (int fish in initialPool)
                startingPool.Add(new Lanternfish(fish));

            for (int i = 0; i < xNumberOfDays; ++i)
            {
                List<Lanternfish> toCreateFish = startingPool.Where(f => f.DaysRemaining == 0).ToList();
                if (toCreateFish.Count != 0)
                {
                    startingPool.Select(f => { f.DaysRemaining--; return f; }).ToList();
                    toCreateFish.Select(f => { f.DaysRemaining = 6; return f; }).ToList();
                    startingPool.AddRange(Enumerable.Range(0, toCreateFish.Count).Select(f => new Lanternfish(8) { }));
                }
                else
                    startingPool.Select(f => { f.DaysRemaining--; return f; }).ToList();
            }

            return startingPool.Count();
        }

        public static long CalculateLanternFishReproudctionRecursively(Dictionary<long, long> fish, int xNumberOfDays)
        {
            if(xNumberOfDays==0)
            {
                return fish.Sum(f=>f.Value);
            }
            else
            {
                long totalNewFish = fish[0];
                fish[0] = fish[1];
                fish[1] = fish[2];
                fish[2] = fish[3];
                fish[3] = fish[4];
                fish[4] = fish[5];
                fish[5] = fish[6];
                fish[6] = fish[7]+totalNewFish;
                fish[7] =fish[8];
                fish[8] = totalNewFish;

                return CalculateLanternFishReproudctionRecursively(fish,xNumberOfDays-1);
            }
        }
    }
}
