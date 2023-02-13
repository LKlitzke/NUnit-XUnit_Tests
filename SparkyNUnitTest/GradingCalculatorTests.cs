using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest
{
    [TestFixture]
    public class GradingCalculatorTests
    {
        private GradingCalculator gradingCalculator;
        [SetUp]
        public void Setup()
        {
            gradingCalculator = new GradingCalculator();
        }

        [Test]
        public void VerifyScoreAndAttendance_InputScore95Attendance90_GetAGrade()
        {
            // Arrange
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            var result = gradingCalculator.GetGrade();

            // Arrange
            Assert.AreEqual(result, "A");

        }

        [Test]
        public void VerifyScoreAndAttendance_InputScore85Attendance90_GetBGrade()
        {
            // Arrange
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            var result = gradingCalculator.GetGrade();

            // Arrange
            Assert.AreEqual(result, "B");

        }

        [Test]
        public void VerifyScoreAndAttendance_InputScore65Attendance90_GetCGrade()
        {
            // Arrange
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            var result = gradingCalculator.GetGrade();

            // Arrange
            Assert.AreEqual(result, "C");

        }

        [Test]
        public void VerifyScoreAndAttendance_InputScore95Attendance65_GetBGrade()
        {
            // Arrange
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;

            // Act
            var result = gradingCalculator.GetGrade();

            // Arrange
            Assert.AreEqual(result, "B");

        }

        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string VerifyScoreAndAttendance_InputScoreAttendance_ReturnsFGrade(int score, int attendance)
        {
            
            // Arrange
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            // Act
            var result = gradingCalculator.GetGrade();

            // Assert
            return result;
        }


    }
}
