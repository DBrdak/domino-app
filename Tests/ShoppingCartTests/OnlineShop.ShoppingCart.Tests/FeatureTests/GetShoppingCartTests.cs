using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.ShoppingCart.Tests.FeatureTests
{
    public class GetShoppingCartTests : BaseIntegrationTest
    {
        public GetShoppingCartTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetShoppingCart_ShouldReturnShoppingCart()
        {
            // Arrange
            var cartId = string.Empty;

            // Act
            var shoppingCart = await Repository.GetShoppingCart(cartId);

            // Assert
            Assert.NotNull(shoppingCart);
        }
    }
}
