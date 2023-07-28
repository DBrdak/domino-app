using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Catalog.API.Entities;

namespace OnlineShop.Catalog.API.Data
{
    public sealed class CatalogContext : DbContext
    {
        public DbSet<Product> Products { get; }

        public CatalogContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.Price);

            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.QuantityModifier);
        }
    }
}