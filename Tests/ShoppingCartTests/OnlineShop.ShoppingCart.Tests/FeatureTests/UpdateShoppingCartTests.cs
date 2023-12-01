using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.ShoppingCart.API.Entities;
using Shared.Domain.Money;
using Shared.Domain.Photo;
using Shared.Domain.Quantity;

namespace OnlineShop.ShoppingCart.Tests.FeatureTests
{
    public class UpdateShoppingCartTests : BaseIntegrationTest
    {
        public UpdateShoppingCartTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task UpdateShoppingCart_ShouldReturnShoppingCart()
        {
            // Arrange
            var cartId = string.Empty;
            var shoppingCart = await Repository.GetShoppingCart(cartId);

            var newShoppingCartItem = new ShoppingCartItem(
                new Quantity(2, Unit.Kg),
                Money.FromString("25,90 zł/kg"),
                "test-product-id",
                "test-product-name",
                new(@"https://res.cloudinary.com/test-product-image"),
                null,
                null);

            shoppingCart.Items.Add(newShoppingCartItem);

            // Act
            await Repository.UpdateShoppingCart(shoppingCart);

            // Assert
            Assert.NotNull(shoppingCart);
            Assert.Contains(newShoppingCartItem, shoppingCart.Items);
        }
    }
}
