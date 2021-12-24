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
    }
}
