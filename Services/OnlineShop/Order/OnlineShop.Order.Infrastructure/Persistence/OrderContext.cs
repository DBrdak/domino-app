using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;

namespace OnlineShop.Order.Infrastructure.Persistence
{

    public class OrderContext : DbContext
    {
        public DbSet<OnlineOrder>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }

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
            SafetyDeleteOrders();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SafetyDeleteOrders()
        {
            var deletedOrders = ChangeTracker.Entries<OnlineOrder>()
                .Where(o => o.State == EntityState.Deleted)
                .Select(x => x.Entity)
                .ToList();

            deletedOrders.ForEach(o => o.SafeDelete());
        }

        private void RejectNotAcceptedOrders()
        {
            var notAcceptedOrders = Orders
                .Where(o => o.DeliveryDate.End.ToUniversalTime() < DateTimeService.UtcNow
                            && (o.Status.StatusMessage != OrderStatus.Received.StatusMessage || o.Status.StatusMessage == OrderStatus.Accepted.StatusMessage || o.Status.StatusMessage == OrderStatus.Modified.StatusMessage))
                .ToList();

            notAcceptedOrders.ForEach(o => o.Reject());
        }

        private void RemoveExpiredOrders()
        {
            var expiredOrders = Orders
                .Where(o => o.ExpiryDate <= DateTimeService.UtcNow);

            Orders.RemoveRange(expiredOrders);
        }
    }
}