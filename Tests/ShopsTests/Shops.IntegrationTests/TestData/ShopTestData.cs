using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Location;
using Shops.Application.Features.Commands.AddShop;

namespace Shops.IntegrationTests.TestData
{
    public class AddShopTestInvalidData : TheoryData<string, MobileShopDto?, StationaryShopDto?>
    {
        public AddShopTestInvalidData()
        {
            Add("", null, new StationaryShopDto(new Location("Test Location", "21.21", "51.51")));
            Add("", new("WPN AA33"), null);
            Add("Invalid Shop", null, null);
        }
    }
}
