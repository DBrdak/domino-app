using Shops.Domain.StationaryShops;

namespace Shops.IntegrationTests.FeatureTests.CommandTests.UpdateShop
{
    public class UpdateStationaryShopTests : UpdateShopTests
    {
        private List<StationaryShop> GetStationaryShops => GetShops.OfType<StationaryShop>().ToList();

        public UpdateStationaryShopTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }
    }
}
