using Moq;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkyXUnitTest
{
    public class ProductXUnitTests
    {
        [Fact]
        public void GetProductPrice_PlatinumCustomer_ReturnPriceWithDiscount()
        {
            Product product = new Product() { Price = 50 };
            var result = product.GetPrice(new Customer() { IsPlatinum = true });

            Assert.Equal(40,result);

        }

        // Não utilizar, somente exemplo de abuso de MOQ
        [Fact]
        public void GetProductPriceMOQAbuse_PlatinumCustomer_ReturnPriceWithDiscount()
        {
            var customer = new Mock<ICustomer>();
            customer.Setup(u => u.IsPlatinum).Returns(true);

            Product product = new Product() { Price = 50 };
            var result = product.GetPrice(customer.Object);

            Assert.Equal(40, result);
        }
    }
}
