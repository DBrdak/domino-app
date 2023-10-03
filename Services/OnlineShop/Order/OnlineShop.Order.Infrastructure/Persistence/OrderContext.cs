using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Date;

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
            RejectNotAcceptedOrders();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void RejectNotAcceptedOrders()
        {
            var notAcceptedOrders = Orders
                .Where(o => o.DeliveryDate.Start < DateTimeService.UtcNow
                            && (o.Status != OrderStatus.Accepted || o.Status != OrderStatus.Received))
                .ToList();

            notAcceptedOrders.ForEach(o => o.Reject());
        }

        private void RemoveExpiredOrders()
        {
            var expiredOrders = Orders
                .Where(o => o.ExpiryDate <= DateTimeService.UtcNow)
                .ToList();

            Orders.RemoveRange(expiredOrders);
        }
    }
}