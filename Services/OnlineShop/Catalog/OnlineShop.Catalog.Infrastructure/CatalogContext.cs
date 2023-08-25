using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OnlineShop.Catalog.Domain;

namespace OnlineShop.Catalog.Infrastructure
{
    public sealed class CatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration config)
        {
            var client = new MongoClient(
                config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(
                config.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(
                config.GetValue<string>("DatabaseSettings:CollectionName"));
        }
    }
}