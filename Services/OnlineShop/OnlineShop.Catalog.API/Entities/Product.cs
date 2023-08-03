﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using OnlineShop.Catalog.API.Models;

namespace OnlineShop.Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Image { get; set; }
        public Money Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDiscounted { get; set; } = false;
        public QuantityModifier QuantityModifier { get; set; }
    }
}