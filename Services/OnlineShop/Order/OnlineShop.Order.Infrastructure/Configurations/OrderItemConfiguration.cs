using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Money;

namespace OnlineShop.Order.Infrastructure.Configurations
{
    internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.OwnsOne(o => o.Price, navBuilder =>
                    {
                        navBuilder.Property(m => m.Currency)
                            .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                        navBuilder.Property(m => m.Unit)
                            .HasConversion(u => u.Code, code => Unit.FromCode(code));
                    }
                );

            builder.OwnsOne(i => i.TotalValue, navBuilder =>
                    {
                        navBuilder.Property(m => m.Currency)
                            .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                        navBuilder.Property(m => m.Unit)
                            .HasConversion(u => u.Code, code => Unit.FromCode(code));
                    }
                );

            builder.OwnsOne(i => i.Quantity, navBuilder =>
                    {
                        navBuilder.Property(q => q.Unit)
                            .HasConversion(u => u.Code, code => Unit.FromCode(code));
                    }
                );
        }
    }
}