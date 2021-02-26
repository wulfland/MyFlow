using System.Collections.Generic;
using System.Linq;

namespace ToRomanNumerals
{
    public static class Extensions
    {
        // bugfix
        static IDictionary<int, string> factors = 
            new Dictionary<int, string>()
        {
            { 1, "I" },
            { 4, "IV" },
            { 5, "V" },
            { 9, "IX" },
            { 10, "X" },
            { 40, "XL" },
            { 50, "L" },
            { 90, "XC" },
            { 100, "C" },
            { 500, "D" },
            { 900, "CM" },
            {1000, "M" }
        };
        
        public static string ToRomanNumerals(this int i)
        {
            if (i <= 0)
                return "";

            int closest = factors.Keys.Last(x => x <= i);

            return factors[closest] + ToRomanNumerals(i - closest);
        }
    }
}

