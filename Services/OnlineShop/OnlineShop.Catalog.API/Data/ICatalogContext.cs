﻿using MongoDB.Driver;
using OnlineShop.Catalog.API.Entities;

namespace OnlineShop.Catalog.API.Data
{
    public interface ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
    }
}