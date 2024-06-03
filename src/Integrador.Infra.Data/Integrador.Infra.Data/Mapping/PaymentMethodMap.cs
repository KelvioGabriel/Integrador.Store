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
    public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethod");
            builder.Property(o => o.Alias).HasMaxLength(50).IsRequired();
            builder.Property(o => o.CardId).HasMaxLength(120).IsRequired();
            builder.Property(o => o.Last4).HasMaxLength(4).IsRequired();
            builder.Property(o => o.ClientId).IsRequired();
        }
    }
}
