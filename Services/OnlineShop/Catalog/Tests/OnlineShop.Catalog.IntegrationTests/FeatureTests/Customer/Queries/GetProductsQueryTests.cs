using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Features.Customer.Queries.GetProducts;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Exceptions;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Customer.Queries
{
    public class GetProductsQueryTests : BaseIntegrationTest
    {
        public GetProductsQueryTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Theory]
        [ClassData(typeof(GetProductsQueryValidTestData))]
        public async void GetProductsAsCustomer_ValidQuery_ShouldReturnFilteredListOfProducts(GetProductsQuery query)
        {
            var result = await Sender.Send(query);

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

            Assert.True(result.Value.Items.All(i => i.Category == Category.FromValue(query.Category)));
            Assert.Equal(result.Value.Page, query.Page);
            Assert.Equal(result.Value.PageSize, query.PageSize);
            Assert.True(result.Value.Items.All(i => i.Price.Amount >= query.MinPrice && i.Price.Amount <= query.MaxPrice));
            Assert.True(result.Value.Items.All(i => i.Details.IsAvailable || !query.IsAvailable));
            Assert.True(result.Value.Items.All(i => !query.IsDiscounted || i.Details.IsDiscounted));
        }


        [Theory]
        [ClassData(typeof(GetProductsQueryInvalidTestData))]
        public async void GetProductsAsCustomer_InvalidQuery_ShouldReturnNonFilteredListOfProducts(GetProductsQuery query)
        {
            var result = await Sender.Send(query);

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

            if(query.Page < 0)
                Assert.Equal(result.Value.Page, query.Page);
            if(query.PageSize < 0)
                Assert.Equal(result.Value.PageSize, query.PageSize);
            if(query.MinPrice < 0 || query.MaxPrice < 0 || (query.MaxPrice < query.MinPrice)) 
                Assert.False(result.Value.Items.All(i => i.Price.Amount >= query.MinPrice && i.Price.Amount <= query.MaxPrice));
        }

        [Fact]
        public async Task GetProductsAsCustomer_InvalidCategoryQuery_ShouldThrow()
        {
            var query = new GetProductsQuery("");

            var queryFunc = async () => await Sender.Send(query);

            await Assert.ThrowsAsync<DomainException<Category>>(queryFunc);
        }
    }
}
