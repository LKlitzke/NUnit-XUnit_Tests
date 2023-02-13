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
    public class CustomerUnitTests
    {
        private Customer customer;
        [SetUp]
        public void Setup()
        {
            customer = new Customer();
        }
        [Test]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            // Arrange
            //var customer = new Customer(); --> Removido pelo Setup

            // Act
            string fullName = customer.GreetAndCombineNames("Ben", "Spark");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(fullName, Is.EqualTo("Hello, Ben Spark"));

                Assert.That(fullName, Does.Contain(","));
                Assert.That(fullName, Does.StartWith("Hello"));
                Assert.That(fullName, Does.EndWith("Spark").IgnoreCase);

                Assert.That(fullName, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
            });

            
        }

        [Test]
        public void GreetMessage_NotGreeted_ReturnsNull()
        {
            // Arrange
            //var customer = new Customer(); --> Removido pelo Setup

            // Act


            // Assert
            Assert.IsNull(customer.GreetMessage);
        }

        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnDiscountInRange()
        {
            int result = customer.Discount;

            Assert.That(result, Is.InRange(10, 25));
        }

        [Test]
        public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
        {
            customer.GreetAndCombineNames("ben", "");

            Assert.IsNotNull(customer.GreetMessage);
            Assert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Test]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            // Com mensagem de erro
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
            Assert.AreEqual("Empty first name", exceptionDetails.Message);

            Assert.That(() => customer.GreetAndCombineNames("", "Spark"),
                Throws.ArgumentException.With.Message.EqualTo("Empty first name"));

            // Sem mensagem de erro
            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));

            Assert.That(() => customer.GreetAndCombineNames("", "Spark"),
                Throws.ArgumentException);

        }

        [Test]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnsBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();

            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnsBasicCustomer()
        {
            customer.OrderTotal = 101;
            var result = customer.GetCustomerDetails();

            Assert.That(result, Is.TypeOf<PlatinumCustomer>());
        }

    }
}
