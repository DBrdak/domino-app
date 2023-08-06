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
                    "Kie�basa Marysie�ki",
                    "Boczek Marysie�ki",
                    "Kaszanka",
                    "Pachwina W�dzona",
                    "Boczek W�dzony",
                    "Salceson Kr�lewski",
                    "Szynka Bia�a Gotowana",
                    "Szynka W�dzona Tradycyjnie",
                    "Pol�dwiczka W�dzona",
                    "Kiszka Ziemiaczana",
                    "Salceson Ozorkowy"
                };

                var meatProducts = new string[]
                {
                    "Kark",
                    "Schab",
                    "�opatka b/k",
                    "Szynka G�rny Zraz",
                    "Szynka Dolny Zraz",
                    "Pol�dwiczka",
                    "�eberka",
                    "Golonka Tylnia",
                    "Pachwina",
                    "Ko�ci Spo�ywcze",
                    "Ko�ci Schabowe"
                };

                var subcategories = new string[]
                {
                    "Podroby",
                    "Kie�basy cienkie",
                    "Kie�basy grube",
                    "Seria Marysie�ki",
                    "Smarowid�a"
                };

                var random = new Random();

                for (int i = 0; i < 10; i++)
                {
                    var sausageProduct = new Product
                    {
                        Name = sausageProducts[i],
                        Description = "Przyk�adowy opis w�dliny",
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
                        Description = "Przyk�adowy opis mi�sa",
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