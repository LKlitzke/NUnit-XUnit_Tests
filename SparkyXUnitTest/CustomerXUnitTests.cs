using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkyXUnitTest
{
    public class CustomerXUnitTests
    {
        [Fact]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            // Arrange
            Customer customer = new Customer();

            // Act
            string fullName = customer.GreetAndCombineNames("Ben", "Spark");

            // Assert
            Assert.Equal("Hello, Ben Spark",customer.GreetMessage);
            Assert.Contains(",",customer.GreetMessage);
            Assert.StartsWith("Hello",customer.GreetMessage);
            Assert.EndsWith("Spark", customer.GreetMessage);

            Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+",customer.GreetMessage);
        }

        [Fact]
        public void GreetMessage_NotGreeted_ReturnsNull()
        {
            // Arrange
            Customer customer = new Customer();

            // Assert
            Assert.Null(customer.GreetMessage);
        }

        [Fact]
        public void DiscountCheck_DefaultCustomer_ReturnDiscountInRange()
        {
            Customer customer = new Customer();
            int result = customer.Discount;

            Assert.InRange(result,10, 25);
        }

        [Fact]
        public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
        {
            Customer customer = new Customer();
            customer.GreetAndCombineNames("ben", "");

            Assert.NotNull(customer.GreetMessage);
            Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Fact]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            Customer customer = new Customer();

            // Com mensagem de erro
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
            Assert.Equal("Empty first name", exceptionDetails.Message);


            // Sem mensagem de erro
            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
        }

        [Fact]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnsBasicCustomer()
        {
            Customer customer = new Customer();
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();

            Assert.IsType<BasicCustomer>(result);
        }

        [Fact]
        public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnsBasicCustomer()
        {
            Customer customer = new Customer();
            customer.OrderTotal = 101;
            var result = customer.GetCustomerDetails();

            Assert.IsType<PlatinumCustomer>(result);
        }

    }
}
