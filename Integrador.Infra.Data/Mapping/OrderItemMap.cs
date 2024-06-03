using Integrador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Infra.Data.Mapping
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.Property(oi => oi.OrderId).IsRequired();
            builder.Property(oi => oi.ProductId).IsRequired();
            builder.Property(oi => oi.ProductName).HasMaxLength(100).IsRequired();
            builder.Property(oi => oi.Amount).IsRequired();
            builder.Property(oi => oi.UnitPrice).HasPrecision(10, 2).IsRequired();
            builder.Property(oi => oi.ProductName).HasMaxLength(250);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .HasConstraintName("Fk_OrderItems_Order")
                .IsRequired();
        }
    }
}
