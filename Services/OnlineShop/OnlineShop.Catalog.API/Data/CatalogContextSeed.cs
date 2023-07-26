using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using OnlineShop.Catalog.API.CustomTypes;
using OnlineShop.Catalog.API.Entities;
using System.Text;

namespace OnlineShop.Catalog.API.Data;

public static class CatalogContextSeed
{
    public static void SeedData(this IMongoCollection<Product> productCollection)
    {
        var isDbEmpty = !productCollection.Find(p => true).Any();

        if (isDbEmpty)
            productCollection.InsertMany(GetPreconfiguredProducts());
    }

    public static Product CreateProduct(string name, string description, string category, decimal price, bool isAvailable, decimal? kgPerPcs)
    {
        return new Product
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = name,
            Description = description,
            Category = category,
            Image = "product_image.jpg",
            Price = new Money(price),
            IsAvailable = isAvailable,
            QuantityModifier = new(kgPerPcs)
        };
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        var products = new List<Product>
        {
            CreateProduct("Kie�basa Marysie�ki", "Kie�basa Marysie�ki jest pyszna i soczysta.", "Sausage", 20.50m, true, null),
            CreateProduct("Boczek Marysie�ki", "Boczek Marysie�ki jest aromatyczny i mi�kki.", "Sausage", 30.00m, true, null),
            CreateProduct("Par�wkowa", "Par�wka w�dzona to doskona�y wyb�r dla dzieci i doros�ych.", "Sausage", 18.75m, true, null),
            CreateProduct("Szynka Tradycyjnie W�dzona", "Szynka w�dzona to klasyczna polska tradycja.", "Sausage", 40.90m, true, null),
            CreateProduct("Kaszanka", "Kaszanka to danie charakterystyczne dla kuchni polskiej.", "Sausage", 15.20m, true, null),
            CreateProduct("Kiszka Ziemiaczana", "Kiszka ziemniaczana to smak dzieci�stwa.", "Sausage", 16.80m, true, null),
            CreateProduct("Par�wki", "Par�wki to popularne danie na grillu i nie tylko.", "Sausage", 21.00m, true, null),
            CreateProduct("Szynka Marysie�ki", "Szynka Marysie�ki jest idealna na kanapki.", "Sausage", 35.25m, false, null),
            CreateProduct("Salceson W�oski", "Salceson w�oski to pyszny dodatek do obiadu.", "Sausage", 19.70m, true, null),
            CreateProduct("Szynka Bia�a Gotowana", "Szynka bia�a gotowana to zdrowy wyb�r dla ca�ej rodziny.", "Sausage", 28.45m, false, null),
            CreateProduct("Schab", "Schab to pyszne mi�so wieprzowe.", "Meat", 25.50m, true, 2.5m),
            CreateProduct("Kark", "Kark to wy�mienite mi�so wieprzowe.", "Meat", 22.90m, true, 2),
            CreateProduct("�opatka b/k", "�opatka bez ko�ci to mi�so idealne do duszenia.", "Meat", 26.75m, false, 4.8m),
            CreateProduct("�ebro", "�eberka to wyj�tkowa przyjemno�� dla mi�o�nik�w mi�sa.", "Meat", 30.80m, true, 1)
        };

        return products;
    }
}