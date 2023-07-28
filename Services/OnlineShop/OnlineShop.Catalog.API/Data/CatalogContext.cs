using System.Text;
using MongoDB.Driver;
using OnlineShop.Catalog.API.Entities;

namespace OnlineShop.Catalog.API.Data
{
    public sealed class CatalogContext : ICatalogContext
    {
        private readonly IWebHostEnvironment _env;

        public CatalogContext(IConfiguration config, IWebHostEnvironment env)
        {
            _env = env;

            var client = new MongoClient(
                config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(
                config.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(
                config.GetValue<string>("DatabaseSettings:CollectionName"));

            if (_env.IsDevelopment())
                Products.SeedData();
        }

        public IMongoCollection<Product> Products { get; }
    }
}