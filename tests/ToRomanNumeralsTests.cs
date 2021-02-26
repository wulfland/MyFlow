using System;
using Xunit;
using ToRomanNumerals;

namespace ToRomanNumeralsTests
{
    public class ToRomanNumeralsTests
    {
        [Theory]
        [InlineData(-1, "")]
        [InlineData(0, "")]
        [InlineData(1, "I")]
        [InlineData(2, "II")]
        [InlineData(3, "III")]
        [InlineData(4, "IV")]
        [InlineData(5, "V")]
        [InlineData(6, "VI")]
        [InlineData(7, "VII")]
        [InlineData(9, "IX")]
        [InlineData(10, "X")]
        [InlineData(11, "XI")]
        [InlineData(12, "XII")]
        [InlineData(42, "XLII")]
        [InlineData(99, "XCIX")]    
        [InlineData(2013, "MMXIII")]
        public void ToRomanNumeralsTest(int input, string expected)
        {
            string result = input.ToRomanNumerals();

            Assert.Equal(expected, result);
        }
    }
}
