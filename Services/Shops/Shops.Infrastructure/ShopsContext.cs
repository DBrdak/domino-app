using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Shops.Domain.Abstractions;

namespace Shops.Infrastructure
{
    public sealed class ShopsContext
    {
        public IMongoCollection<Shop> Shops { get; }

        public ShopsContext(IConfiguration config)
        {
            var client = new MongoClient(
                config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(
                config.GetValue<string>("DatabaseSettings:DatabaseName"));

            Shops = database.GetCollection<Shop>(
                config.GetValue<string>("DatabaseSettings:Collections:Shops"));
        }
    }
}