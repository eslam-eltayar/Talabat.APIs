using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Infrastructure._Data.Config.Order_Config
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

            builder.Property(order => order.Status)
                    .HasConversion(
                        (OStatus) => OStatus.ToString(),
                        (OStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                        );

            builder.HasOne(order => order.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(order => order.SubTotal).HasColumnType("decimal(12,2)");

            builder.HasMany(order => order.Items)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
