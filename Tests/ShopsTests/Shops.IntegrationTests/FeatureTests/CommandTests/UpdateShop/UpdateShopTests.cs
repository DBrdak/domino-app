using MongoDB.Driver;
using Shops.Application.Features.Commands.UpdateShop;
using Shops.Domain.MobileShops;
using Shops.Domain.Shared;
using Shops.Domain.Shops;
using Shops.Domain.StationaryShops;

namespace Shops.IntegrationTests.FeatureTests.CommandTests.UpdateShop
{
    public class UpdateShopTests : BaseIntegrationTest
    {
        private readonly string _shopId;
        protected List<Shop> GetShops => Context.Shops.Find(FilterDefinition<Shop>.Empty).ToList();

        public UpdateShopTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            _shopId = GetShops[0].Id;
        }

        [Fact]
        public async Task AddSeller_ValidData_ShouldSuccess()
        {
            // Arrange
            var newSeller = new Seller("Test", "Tester", "555444333");
            var command = new UpdateShopCommand(
                _shopId,
                newSeller,
                null,
                null,
                null);

            // Act
            var result = await Sender.Send(command);
            var isSellerAdded = GetShops.First(s => s.Id == _shopId).Sellers.Contains(newSeller);

            // Assert
            Assert.True(isSellerAdded);
        }

        [Fact]
        public async Task DeleteSeller_ValidData_ShouldSuccess()
        {
            // Arrange
            var sellerToDelete = GetShops.First(s => s.Id == _shopId).Sellers[0];
            var command = new UpdateShopCommand(
                _shopId,
                null,
                sellerToDelete,
                null,
                null);

            // Act
            var result = await Sender.Send(command);
            var isSellerDeleted = !GetShops.First(s => s.Id == _shopId).Sellers.Contains(sellerToDelete);

            // Assert
            Assert.True(isSellerDeleted);
        }
    }
}
