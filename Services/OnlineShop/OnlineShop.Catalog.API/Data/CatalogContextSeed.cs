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
            CreateProduct("Kie³basa Marysieñki", "Kie³basa Marysieñki jest pyszna i soczysta.", "Sausage", 20.50m, true, null),
            CreateProduct("Boczek Marysieñki", "Boczek Marysieñki jest aromatyczny i miêkki.", "Sausage", 30.00m, true, null),
            CreateProduct("Parówkowa", "Parówka wêdzona to doskona³y wybór dla dzieci i doros³ych.", "Sausage", 18.75m, true, null),
            CreateProduct("Szynka Tradycyjnie Wêdzona", "Szynka wêdzona to klasyczna polska tradycja.", "Sausage", 40.90m, true, null),
            CreateProduct("Kaszanka", "Kaszanka to danie charakterystyczne dla kuchni polskiej.", "Sausage", 15.20m, true, null),
            CreateProduct("Kiszka Ziemiaczana", "Kiszka ziemniaczana to smak dzieciñstwa.", "Sausage", 16.80m, true, null),
            CreateProduct("Parówki", "Parówki to popularne danie na grillu i nie tylko.", "Sausage", 21.00m, true, null),
            CreateProduct("Szynka Marysieñki", "Szynka Marysieñki jest idealna na kanapki.", "Sausage", 35.25m, false, null),
            CreateProduct("Salceson W³oski", "Salceson w³oski to pyszny dodatek do obiadu.", "Sausage", 19.70m, true, null),
            CreateProduct("Szynka Bia³a Gotowana", "Szynka bia³a gotowana to zdrowy wybór dla ca³ej rodziny.", "Sausage", 28.45m, false, null),
            CreateProduct("Schab", "Schab to pyszne miêso wieprzowe.", "Meat", 25.50m, true, 2.5m),
            CreateProduct("Kark", "Kark to wyœmienite miêso wieprzowe.", "Meat", 22.90m, true, 2),
            CreateProduct("£opatka b/k", "£opatka bez koœci to miêso idealne do duszenia.", "Meat", 26.75m, false, 4.8m),
            CreateProduct("¯ebro", "¯eberka to wyj¹tkowa przyjemnoœæ dla mi³oœników miêsa.", "Meat", 30.80m, true, 1)
        };

        return products;
    }
}