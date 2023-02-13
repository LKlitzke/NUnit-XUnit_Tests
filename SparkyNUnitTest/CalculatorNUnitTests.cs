using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CalculatorNUnitTests
    {
        private Calculator calc;
        [SetUp]
        public void Setup()
        {
            calc = new Calculator();
        }

        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            // Arrange
            //Calculator calc = new(); --> Removido pelo Setup

            // Act
            int result = calc.AddNumbers(10, 20);

            // Assert
            Assert.AreEqual(30, result);

        }

        [Test]
        public void IsOddNumber_InputOneInt_GetCorrectType()
        {
            // Arrange
            //Calculator calc = new(); --> Removido pelo Setup

            // Act
            bool result = calc.IsOddNumber(10);

            // Assert
            Assert.IsFalse(result);
            Assert.That(result, Is.EqualTo(false));

        }

        [Test]
        [TestCase(11)]
        [TestCase(13)]
        public void IsOddNumber_InputOneInt_GetInCorrectType(int a)
        {
            // Arrange
            //Calculator calc = new(); --> Removido pelo Setup

            // Act
            bool result = calc.IsOddNumber(a);

            // Assert
            Assert.IsTrue(result);
            Assert.That(result, Is.EqualTo(true));

        }

        [Test]
        [TestCase(10,ExpectedResult = false)]
        [TestCase(11, ExpectedResult = true)]
        public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int a)
        {
            Calculator calc = new();
            return calc.IsOddNumber(a);
        }

        [Test]
        [TestCase(5.4,10.5)]
        [TestCase(12.32, 11.53)]
        [TestCase(9.0522, 0.0032)]
        public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            // Arrange
            Calculator calc = new();

            // Act
            double result = calc.AddNumbersDouble(a, b);

            // Assert
            // Delta = aproximação de valor
            Assert.AreEqual(15.9, result,10);

        }

        [Test]
        public void OddRanger_InputMinAndMaxRange_ReturnsValidOddNumberRange()
        {
            // Arrange
            List<int> expectedRange = new() { 5, 7, 9 };

            // Act
            List<int> result = calc.GetOddRange(5, 10);

            // Assert
            Assert.That(result, Is.EquivalentTo(expectedRange));
            Assert.AreEqual(result,expectedRange);

            Assert.Contains(7, result);
            Assert.That(result, Does.Contain(7));
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result, Has.No.Member(6));

            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }
    }
}
