using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkyXUnitTest
{
    public class CalculatorXUnitTests
    {
        [Fact]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            // Arrange
            Calculator calc = new();

            // Act
            int result = calc.AddNumbers(10, 20);

            // Assert
            Assert.Equal(30, result);

        }

        [Fact]
        public void IsOddNumber_InputOneInt_GetCorrectType()
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool result = calc.IsOddNumber(10);

            // Assert
            Assert.False(result);
            //Assert.That(result, Is.EqualTo(false));

        }

        [Theory]
        [InlineData(11)]
        [InlineData(13)]
        public void IsOddNumber_InputOneInt_GetInCorrectType(int a)
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool result = calc.IsOddNumber(a);

            // Assert
            Assert.True(result);
            //Assert.That(result, Is.EqualTo(true));

        }

        [Theory]
        [InlineData(10, false)]
        [InlineData(11, true)]
        public void IsOddChecker_InputNumber_ReturnTrueIfOdd(int a, bool expectedResult)
        {
            Calculator calc = new();
            var result = calc.IsOddNumber(a);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5.4, 10.5)]
        //[InlineData(5.43, 10.53)]
        //[InlineData(5.49, 10.59)]
        public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            // Arrange
            Calculator calc = new();

            // Act
            double result = calc.AddNumbersDouble(a, b);

            // Assert
            // Precision = aproximação de valor
            Assert.Equal(15.9, result, 1);

        }

        [Fact]
        public void OddRanger_InputMinAndMaxRange_ReturnsValidOddNumberRange()
        {
            // Arrange
            Calculator calc = new();
            List<int> expectedRange = new() { 5, 7, 9 };

            // Act
            List<int> result = calc.GetOddRange(5, 10);

            // Assert
            Assert.Equal(expectedRange, result);

            Assert.Contains(7, result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
            Assert.DoesNotContain(6, result);

            Assert.Equal(result.OrderBy(u => u), result);
            //Assert.(result, Is.Unique);
        }
    }
}
