using Bongo.Models.ModelValidations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Models
{
    [TestFixture]
    public class DateInFutureAttributesTests
    {
        [Test]
        [TestCase(100,ExpectedResult = true)]
        [TestCase(-100, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        public bool DateValidaor_InputExpectedDateRange_DateValidity(int addTime)
        {
            DateInFutureAttribute dateInFutureAttribute = new (() => DateTime.Now);

            return dateInFutureAttribute.IsValid(DateTime.Now.AddSeconds(addTime));
        }

        [Test]
        public void DateValidaor_NotValid_Date_ReturnsErrorMessage()
        {
            var result = new DateInFutureAttribute();
            Assert.AreEqual("Date must be in the future", result.ErrorMessage);
        }
    }
}
