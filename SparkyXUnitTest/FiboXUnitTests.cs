using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace SparkyXUnitTest
{
    public class FiboXUnitTests
    {

        [Fact]
        public void ListTests_InputRange1_ReturnsTrue()
        {
            // Arrange
            Fibo fibo = new Fibo();
            fibo.Range = 1;
            List<int> expectedValues = new List<int>() { 0 };

            // Act
            List<int> list = fibo.GetFiboSeries();

            // Assert
            Assert.NotEmpty(list);
            Assert.Equal(expectedValues.OrderBy(u => u), list);
            Assert.True(list.SequenceEqual(expectedValues));
        }

        [Fact]
        public void ListTests_InputRange6_ReturnsTrue()
        {
            // Arrange
            Fibo fibo = new Fibo();
            fibo.Range = 6;
            List<int> expectedValues = new List<int>() { 0, 1, 1, 2, 3, 5 };

            // Act
            List<int> list = fibo.GetFiboSeries();

            // Assert
            Assert.Contains(3,list);
            Assert.Equal(6,list.Count);
            Assert.DoesNotContain(4,list);
            Assert.True(list.SequenceEqual(expectedValues));
        }

    }
}
