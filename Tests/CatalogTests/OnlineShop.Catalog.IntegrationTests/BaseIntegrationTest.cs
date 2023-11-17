using CloudinaryDotNet;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Domain.Shared;
using OnlineShop.Catalog.Infrastructure;
using OnlineShop.Catalog.Infrastructure.Repositories;
using Shared.Domain.Money;
using Shared.Domain.Photo;
using Unit = Shared.Domain.Money.Unit;

namespace OnlineShop.Catalog.IntegrationTests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly CatalogContext Context;
    protected readonly IPhotoRepository PhotoRepository;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        Context = _scope.ServiceProvider.GetRequiredService<CatalogContext>();
        PhotoRepository = _scope.ServiceProvider.GetRequiredService<IPhotoRepository>();

        if (Context.Products.EstimatedDocumentCount() < 1 &&
            Context.PriceLists.EstimatedDocumentCount() < 1)
        {
            SeedDatabase();
        }
    }

    private void SeedDatabase()
    {
        var factory = new DocumentFactory();

        var priceLists = factory.GetPrefabricatedPriceLists();
        var products = factory.GetPrefabricatedProducts();
        var businessPriceLists = factory.GetPrefabricatedBusinessPriceLists();

        Context.PriceLists.InsertMany(priceLists);
        Context.Products.InsertMany(products);
        Context.PriceLists.InsertMany(businessPriceLists);
    }

    public class DocumentFactory
    {
        public List<Product> GetPrefabricatedProducts() => _products;
        public List<PriceList> GetPrefabricatedPriceLists() => _priceLists;
        public List<PriceList> GetPrefabricatedBusinessPriceLists() => _businessPriceLists;

        private readonly List<Product> _products;
        private readonly List<PriceList> _priceLists;
        private readonly List<PriceList> _businessPriceLists;

        public DocumentFactory()
        {
            _priceLists = CreatePriceLists();
            _products = CreateProducts();
            _businessPriceLists = CreateBusinessPriceLists();
        }

        private List<Product> CreateProducts()
        {
            var products = new List<Product>();

            for (var i = 1; i <= 6; i++)
            {
                var product = CreateProduct(i);

                products.Add(product);
            }

            return products;
        }

        private Product CreateProduct(int index)
        {
            var createValues = new CreateValues(
                _priceLists[(index - 1) % 2].LineItems[index - 1].Name,
                $"Opis {index}",
                index % 3 == 0,
                index % 3 == 0 ? index * 2.4m : null
            );
            var category = _priceLists[(index - 1) % 2].Category;
            var price = _priceLists[(index - 1) % 2].LineItems[index - 1].Price;
            var image = $"https://res.cloudinary.com/sampleImage{index}";
            createValues.AttachCategory(category);
            createValues.AttachImage(image);
            createValues.AttachPrice(price);

            var product = Domain.Products.Product.Create(createValues);
            _priceLists[(index - 1) % 2].AggregateLineItemWithProduct(product.Name, product.Id);
            return product;
        }

        private List<PriceList> CreatePriceLists()
        {
            var priceLists = new List<PriceList>()
            {
                PriceList.CreateRetail("Cennik detaliczny wędlin", Category.Sausage),
                PriceList.CreateRetail("Cennik detaliczny mięsa", Category.Meat)
            };

            priceLists.ForEach(
                pl =>
                {
                    for (var i = 1; i < 10; i++)
                    {
                        pl.AddLineItem(
                            new(
                                $"{pl.Category.Value} {i}",
                                new Money(4.9m * i, Currency.Pln, i % 2 == 0 ? Unit.Kg : Unit.Pcs)
                            ));
                    }
                });
            return priceLists;
        }

        private List<PriceList> CreateBusinessPriceLists()
        {
            var priceLists = new List<PriceList>()
            {
                PriceList.CreateBusiness("Cennik hurtowy wędlin MiniButcher", "MiniButcher", Category.Sausage),
                PriceList.CreateBusiness("Cennik hurtowy mięsa MiniButcher", "MiniButcher", Category.Meat)
            };

            return priceLists;
        }
    }
}