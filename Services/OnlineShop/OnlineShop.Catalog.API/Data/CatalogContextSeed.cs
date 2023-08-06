using System;
using MongoDB.Driver;
using OnlineShop.Catalog.API.Entities;
using OnlineShop.Catalog.API.Models;

namespace OnlineShop.Catalog.API.Data
{
    public static class CatalogContextSeed
    {
        public static void SeedData(this IMongoCollection<Product> productCollection)
        {
            if (!productCollection.Find(p => true).Any())
            {
                var sausageProducts = new string[]
                {
                    "Kie³basa Marysieñki",
                    "Boczek Marysieñki",
                    "Kaszanka",
                    "Pachwina Wêdzona",
                    "Boczek Wêdzony",
                    "Salceson Królewski",
                    "Szynka Bia³a Gotowana",
                    "Szynka Wêdzona Tradycyjnie",
                    "Polêdwiczka Wêdzona",
                    "Kiszka Ziemiaczana",
                    "Salceson Ozorkowy"
                };

                var meatProducts = new string[]
                {
                    "Kark",
                    "Schab",
                    "£opatka b/k",
                    "Szynka Górny Zraz",
                    "Szynka Dolny Zraz",
                    "Polêdwiczka",
                    "¯eberka",
                    "Golonka Tylnia",
                    "Pachwina",
                    "Koœci Spo¿ywcze",
                    "Koœci Schabowe"
                };

                var subcategories = new string[]
                {
                    "Podroby",
                    "Kie³basy cienkie",
                    "Kie³basy grube",
                    "Seria Marysieñki",
                    "Smarowid³a"
                };

                var random = new Random();

                for (int i = 0; i < 10; i++)
                {
                    var sausageProduct = new Product
                    {
                        Name = sausageProducts[i],
                        Description = "Przyk³adowy opis wêdliny",
                        Category = "Sausage",
                        Subcategory = subcategories[random.Next(0, subcategories.Length)],
                        Image = "sausage-image.jpg",
                        Price = new Money(random.Next(15, 45)),
                        IsAvailable = i % 2 == 0,
                        IsDiscounted = i % 3 == 0,
                        QuantityModifier = i % 3 == 0 ? new QuantityModifier() : new QuantityModifier((decimal)random.NextDouble() * random.Next(1, 3))
                    };
                    productCollection.InsertOne(sausageProduct);

                    var meatProduct = new Product
                    {
                        Name = meatProducts[i],
                        Description = "Przyk³adowy opis miêsa",
                        Category = "Meat",
                        Subcategory = "",
                        Image = "meat-image.jpg",
                        Price = new Money(random.Next(15, 45)),
                        IsAvailable = i % 2 == 0,
                        IsDiscounted = i % 3 == 0,
                        QuantityModifier = new QuantityModifier((decimal)random.NextDouble() * random.Next(1, 3))
                    };
                    productCollection.InsertOne(meatProduct);
                }
            }
        }
    }
}