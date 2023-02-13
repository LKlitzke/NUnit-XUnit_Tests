using Moq;
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
    public class ProductNUnitTests
    {
        [Test]
        public void GetProductPrice_PlatinumCustomer_ReturnPriceWithDiscount()
        {
            Product product = new Product() { Price = 50 };
            var result = product.GetPrice(new Customer() { IsPlatinum = true });

            Assert.That(result,Is.EqualTo(40));

        }

        // Não utilizar, somente exemplo de abuso de MOQ
        [Test]
        public void GetProductPriceMOQAbuse_PlatinumCustomer_ReturnPriceWithDiscount()
        {
            var customer = new Mock<ICustomer>();
            customer.Setup(u => u.IsPlatinum).Returns(true);

            Product product = new Product() { Price = 50 };
            var result = product.GetPrice(customer.Object);

            Assert.That(result, Is.EqualTo(40));

        }
    }
}
