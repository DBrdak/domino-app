using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddRetailPriceList;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Errors;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists
{
    public class AddRetailPriceListTests : BaseIntegrationTest
    {
        public AddRetailPriceListTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task AddRetailPriceList_ValidData_ShouldCreateAndAddToDb()
        {
            // Arrange
            await Context.PriceLists.DeleteManyAsync(FilterDefinition<PriceList>.Empty);
            var command = new AddRetailPriceListCommand();

            // Act
            var result = await Sender.Send(command);
            var isBothPriceListCategoriesAppendedToDb = (await Context.PriceLists.FindAsync(pl => pl.Contractor == Contractor.Retail)).ToList().Count == 2;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            Assert.True(isBothPriceListCategoriesAppendedToDb);
        }
    }
}
