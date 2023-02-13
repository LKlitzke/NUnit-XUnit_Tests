using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SparkyNUnitTest
{
    [TestFixture]
    public class FiboTests
    {
        private Fibo fibo;
        [SetUp]
        public void Setup()
        {
            fibo = new Fibo();
        }

        [Test]
        public void ListTests_InputRange1_ReturnsTrue()
        {
            // Arrange
            fibo.Range = 1;
            List<int> expectedValues = new List<int>() { 0 };

            // Act
            List<int> list = fibo.GetFiboSeries();

            // Assert
            Assert.That(list, Is.Not.Empty);
            Assert.That(list, Is.Ordered);
            Assert.That(list, Is.EquivalentTo(expectedValues));
        }

        [Test]
        public void ListTests_InputRange6_ReturnsTrue()
        {
            // Arrange
            fibo.Range = 6;
            List<int> expectedValues = new List<int>() { 0,1,1,2,3,5 };

            // Act
            List<int> list = fibo.GetFiboSeries();

            // Assert
            Assert.That(list, Does.Contain(3));
            Assert.That(list.Count, Is.EqualTo(6));
            Assert.That(list, Does.Not.Contain(4));
            Assert.That(list, Is.EquivalentTo(expectedValues));
        }

    }
}
