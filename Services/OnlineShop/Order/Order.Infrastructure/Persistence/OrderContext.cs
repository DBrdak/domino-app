using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public DbSet<Domain.Entities.Order> Orders { get; set; }

        public OrderContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}