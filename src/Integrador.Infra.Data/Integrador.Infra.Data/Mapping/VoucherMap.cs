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
    public class VoucherMap : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Voucher");
            builder.Property(v => v.Code).HasMaxLength(5);
            builder.Property(v => v.DiscountType).IsRequired();
            builder.Property(v => v.Amount).IsRequired();
            builder.Property(v => v.AmountUsed).HasDefaultValue(0);
            builder.Property(v => v.ExpirationDate).IsRequired();
            builder.Property(v => v.Active).HasDefaultValue(true);
            builder.Property(v => v.Value).HasPrecision(10, 2).IsRequired();

        }
    }
}
