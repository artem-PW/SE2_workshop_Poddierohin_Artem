using System;
using Xunit;
using StringCalculator;

namespace StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        private readonly StringCalculator _calculator = new StringCalculator();

        [Theory]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        public void Calculate_EmptyOrNullString_ReturnsZero(string input, int expected)
        {
            int result = _calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("5", 5)]
        [InlineData("42", 42)]
        [InlineData("999", 999)]
        public void Calculate_SingleNumber_ReturnsThatNumber(string input, int expected)
        {
            int result = _calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("0,0", 0)]
        [InlineData("10,20", 30)]
        [InlineData("100,200", 300)]
        public void Calculate_TwoNumbersCommaDelimited_ReturnsSum(string input, int expected)
        {
            int result = _calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1\n2", 3)]
        [InlineData("10\n20", 30)]
        [InlineData("0\n5", 5)]
        public void Calculate_TwoNumbersNewlineDelimited_ReturnsSum(string input, int expected)
        {
            int result = _calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1,2,3", 6)]
        [InlineData("1\n2\n3", 6)]
        [InlineData("1,2\n3", 6)]
        [InlineData("1\n2,3", 6)]
        [InlineData("10,20,30", 60)]
        public void Calculate_ThreeNumbersMixedDelimiters_ReturnsSum(string input, int expected)
        {
            int result = _calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("-1", "-1")]
        [InlineData("-1,-2", "-1, -2")]
        [InlineData("1,-2,3,-4", "-2, -4")]
        public void Calculate_NegativeNumbers_ThrowsException(
            string input, string expectedNegatives)
        {
            var exception = Assert.Throws<ArgumentException>(
                () => _calculator.Calculate(input)
            );

            Assert.Contains(expectedNegatives, exception.Message);
        }

        [Theory]
        [InlineData("1001", 0)]
        [InlineData("2,1001", 2)]
        [InlineData("1000,1001,2", 1002)]
        [InlineData("1000,2", 1002)]
        [InlineData("5000,3,2000,7", 10)]
        public void Calculate_NumbersGreaterThan1000_AreIgnored(string input, int expected)
        {
            int result = _calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//#\n1#2", 3)]
        [InlineData("//;\n1;2;3", 6)]
        [InlineData("//|\n10|20|30", 60)]
        [InlineData("//_\n5_5", 10)]
        public void Calculate_SingleCharCustomDelimiter_ReturnsSum(string input, int expected)
        {
            int result = _calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        
        [Theory]
        [InlineData("//[###]\n1###2###3", 6)]
        [InlineData("//[**]\n10**20", 30)]
        [InlineData("//[abc]\n5abc10abc15", 30)]
        [InlineData("//[;;]\n100;;200;;300", 600)]
        public void Calculate_MultiCharCustomDelimiter_ReturnsSum(string input, int expected)
        {
            int result = _calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//[#][%]\n1#2%3", 6)]
        [InlineData("//[##][%%]\n1##2%%3", 6)]
        [InlineData("//[*][;][|]\n1*2;3|4", 10)]
        [InlineData("//[ab][cd]\n10ab20cd30", 60)]
        public void Calculate_ManyCustomDelimiters_ReturnsSum(string input, int expected)
        {
            int result = _calculator.Calculate(input);

            Assert.Equal(expected, result);
        }
    }
}