using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Money;

namespace OnlineShop.Order.Infrastructure.Configurations
{
    internal sealed class OnlineOrderConfiguration : IEntityTypeConfiguration<OnlineOrder>
    {
        public void Configure(EntityTypeBuilder<OnlineOrder> builder)
        {
            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(o => o.Id);

            builder.OwnsOne(o => o.DeliveryDate);

            builder.OwnsOne(o => o.DeliveryLocation);

            builder.OwnsOne(o => o.TotalPrice, navBuilder =>
                {
                    navBuilder.Property(m => m.Currency)
                        .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                    navBuilder.Property(m => m.Unit)
                        .HasConversion(u => u.Code, code => Unit.FromCode(code));
                });

            builder.OwnsOne(o => o.Status);
        }
    }
}