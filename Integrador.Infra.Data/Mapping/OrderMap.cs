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
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.Property(o => o.ClientId).IsRequired();
            builder.Property(o => o.AddressId).IsRequired();
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.Code).HasMaxLength(5).IsRequired();
            builder.Property(o => o.PaymentMethodId).IsRequired();
            builder.Property(o => o.TotalValue).HasPrecision(10, 2);
            builder.Property(o => o.Discount).HasPrecision(10, 2);
        }
    }
}
