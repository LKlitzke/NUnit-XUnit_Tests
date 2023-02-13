using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkyXUnitTest
{
    public class GradingCalculatorXUnitTests
    {

        [Fact]
        public void VerifyScoreAndAttendance_InputScore95Attendance90_GetAGrade()
        {
            // Arrange
            GradingCalculator gradingCalculator = new GradingCalculator();
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            var result = gradingCalculator.GetGrade();

            // Arrange
            Assert.Equal(result, "A");

        }

        [Fact]
        public void VerifyScoreAndAttendance_InputScore85Attendance90_GetBGrade()
        {
            // Arrange
            GradingCalculator gradingCalculator = new GradingCalculator();
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            var result = gradingCalculator.GetGrade();

            // Arrange
            Assert.Equal(result, "B");

        }

        [Fact]
        public void VerifyScoreAndAttendance_InputScore65Attendance90_GetCGrade()
        {
            // Arrange
            GradingCalculator gradingCalculator = new GradingCalculator();
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            var result = gradingCalculator.GetGrade();

            // Arrange
            Assert.Equal(result, "C");

        }

        [Fact]
        public void VerifyScoreAndAttendance_InputScore95Attendance65_GetBGrade()
        {
            // Arrange
            GradingCalculator gradingCalculator = new GradingCalculator();
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;

            // Act
            var result = gradingCalculator.GetGrade();

            // Arrange
            Assert.Equal(result, "B");

        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 65, "B")]
        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void VerifyScoreAndAttendance_InputScoreAttendance_ReturnsFGrade(int score, int attendance, string expectedResult)
        {
            // Arrange
            GradingCalculator gradingCalculator = new GradingCalculator();
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            // Act
            var result = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal(expectedResult, result);
        }


    }
}
