using Microsoft.EntityFrameworkCore;
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);
            base.OnModelCreating(modelBuilder);
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