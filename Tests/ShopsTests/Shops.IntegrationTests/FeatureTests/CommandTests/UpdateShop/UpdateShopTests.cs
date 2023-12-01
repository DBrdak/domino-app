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
        protected readonly string MobileShopId;
        protected readonly string StationaryShopId;
        protected List<Shop> GetShops => Context.Shops.Find(FilterDefinition<Shop>.Empty).ToList();

        public UpdateShopTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            var shops = Context.Shops.Find(FilterDefinition<Shop>.Empty).ToList();
            MobileShopId = shops.First(s => s is MobileShop).Id;
            StationaryShopId = shops.First(s => s is StationaryShop).Id;
        }

        [Fact]
        public async Task AddSeller_ValidData_ShouldSuccess()
        {
            // Arrange
            var newSeller = new Seller("Test", "Tester", "555444333");
            var command = new UpdateShopCommand(
                MobileShopId,
                newSeller,
                null,
                null,
                null);

            // Act
            var result = await Sender.Send(command);
            var isSellerAdded = GetShops.First(s => s.Id == MobileShopId).Sellers.Contains(newSeller);

            // Assert
            Assert.True(isSellerAdded);
        }

        [Fact]
        public async Task DeleteSeller_ValidData_ShouldSuccess()
        {
            // Arrange
            var sellerToDelete = GetShops.First(s => s.Id == MobileShopId).Sellers[0];
            var command = new UpdateShopCommand(
                MobileShopId,
                null,
                sellerToDelete,
                null,
                null);

            // Act
            var result = await Sender.Send(command);
            var isSellerDeleted = !GetShops.First(s => s.Id == MobileShopId).Sellers.Contains(sellerToDelete);

            // Assert
            Assert.True(isSellerDeleted);
        }
    }
}
