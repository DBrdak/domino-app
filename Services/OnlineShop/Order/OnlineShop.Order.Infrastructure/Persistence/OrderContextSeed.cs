using OnlineShop.Order.Domain.Common;
using OnlineShop.Order.Domain.Entities;

namespace OnlineShop.Order.Infrastructure.Persistence
{
    public static class OrderContextSeed
    {
        public static void Seed(this OrderContext context)
        {
            //if (context.Set<OnlineOrder>().Any())
            //    return;

            //var order1 = new OnlineOrder
            //{
            //    // Shopping Cart Info
            //    TotalPrice = 100.0m,
            //    Items = new List<OrderItem>
            //    {
            //        new OrderItem { Quantity = 2, Unit = "kg", ProductName = "Boczek Marysieńki", Status = "Nie" },
            //        new OrderItem { Quantity = 3, Unit = "szt", ProductName = "Product B", Status = "Nie" }
            //    },

            //    // Personal Info
            //    PhoneNumber = "501001001",
            //    FirstName = "John",
            //    LastName = "Doe",

            //    // Delivery Info
            //    DeliveryLocation = new Location("Address A", "51.500152", "-0.126236"),
            //    DeliveryDate = new DateTimeRange(DateTime.UtcNow.AddDays(2).AddHours(-10), DateTime.UtcNow.AddDays(2).AddHours(-9)),

            //    // Order Info
            //    Status = "Oczekuje na potwierdzenie"
            //};

            //var order2 = new OnlineOrder
            //{
            //    // Shopping Cart Info
            //    TotalPrice = 110.0m,
            //    Items = new List<OrderItem>
            //    {
            //        new OrderItem { Quantity = 1, Unit = "szt", ProductName = "Produkt C", Status = "Tak" },
            //        new OrderItem { Quantity = 4, Unit = "szt", ProductName = "Produkt A", Status = "Tylko 2 sztuki" }
            //},

            //    // Personal Info
            //    PhoneNumber = "502002002",
            //    FirstName = "Jane",
            //    LastName = "Smith",

            //    // Delivery Info
            //    DeliveryLocation = new Location("Address B", "52.500152", "-1.126236"),
            //    DeliveryDate = new DateTimeRange(DateTime.UtcNow.AddDays(1).AddHours(-7), DateTime.UtcNow.AddDays(1).AddHours(-6)),

            //    // Order Info
            //    Status = "Potwierdzone - kompletujemy twoje zamówienie"
            //};

            //var orders = new[] { order1, order2 };

            //context.Orders.AddRange(orders);
            //context.SaveChanges();
        }
    }
}