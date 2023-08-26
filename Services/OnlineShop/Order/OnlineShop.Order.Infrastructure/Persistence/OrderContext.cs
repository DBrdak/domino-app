using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Domain;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Money;

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

            modelBuilder.Entity<OrderItem>()
                .OwnsOne(o => o.Price, builder =>
                    {
                        builder.Property(m => m.Currency)
                            .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                        builder.Property(m => m.Unit)
                            .HasConversion(u => u.Code, code => Unit.FromCode(code));
                    }
                );

            modelBuilder.Entity<OrderItem>()
                .OwnsOne(i => i.TotalValue, builder =>
                    {
                        builder.Property(m => m.Currency)
                            .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                        builder.Property(m => m.Unit)
                            .HasConversion(u => u.Code, code => Unit.FromCode(code));
                    }
                );

            modelBuilder.Entity<OrderItem>()
                .OwnsOne(i => i.Quantity, builder =>
                    {
                        builder.Property(q => q.Unit)
                            .HasConversion(u => u.Code, code => Unit.FromCode(code));
                    }
                );

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

            modelBuilder.Entity<OnlineOrder>()
                .OwnsOne(o => o.TotalPrice, builder =>
                {
                    builder.Property(m => m.Currency)
                        .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                    builder.Property(m => m.Unit)
                        .HasConversion(u => u.Code, code => Unit.FromCode(code));
                });

            modelBuilder.Entity<OnlineOrder>()
                .OwnsOne(o => o.Status);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            RemoveExpiredOrders();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void RemoveExpiredOrders()
        {
            var expiredOrders = ChangeTracker.Entries<OnlineOrder>()
                .Where(e => e.Entity.ExpiryDate <= DateTime.UtcNow && e.State != EntityState.Deleted)
                .ToList();

            foreach (var entry in expiredOrders)
            {
                entry.State = EntityState.Deleted;
            }
        }
    }
}