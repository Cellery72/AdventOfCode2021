using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Models
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Removes duplicate spaces from a string 
        /// </summary>
        /// <param name="str">string to remove duplicates from</param>
        /// <returns></returns>
        public static string CollapseWhitespace(this string str)
        {
            string returnValue = string.Empty;
            char previousChar = '!';
            foreach (char c in str)
            {
                if (previousChar == ' ' && c == ' ')
                    continue;
                else
                    returnValue += c;

                previousChar = c;
            }
            return returnValue.Trim();
        }

        public static long Factorial(this long f)
        {
            if (f == 0)
                return 1;
            else
                return f * (Factorial(f - 1));
        }

        public static long CalculateExpontentialSteps(this long f, long newPosition)
        {
            long result = 0, previous = -1;
            long movesRequired = (f > newPosition) ? f - newPosition : newPosition - f;

            for (long i = 1; i <= movesRequired; ++i)
            {
                result += (previous != -1) ? i : 1;
                previous = i;
            }

            return result;
        }
    }
}
