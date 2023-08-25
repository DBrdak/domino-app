using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Domain;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OrderItems;

namespace OnlineShop.Order.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public DbSet<OnlineOrder> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<OnlineOrder>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OnlineOrder>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OnlineOrder>()
                .OwnsOne(o => o.DeliveryDate);

            modelBuilder.Entity<OnlineOrder>()
                .OwnsOne(o => o.DeliveryLocation);

            modelBuilder.Entity<OrderItem>()
                .OwnsOne(o => o.Price);
        }
    }
}