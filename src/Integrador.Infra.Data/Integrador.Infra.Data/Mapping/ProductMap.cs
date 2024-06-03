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
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(250);
            builder.Property(p => p.Active).HasDefaultValue(true).IsRequired();
            builder.Property(p => p.Price).HasPrecision(10, 2).IsRequired();
            builder.Property(p => p.Image).HasMaxLength(250);
            builder.Property(p => p.StockQuantity).IsRequired();
            builder.Property(p => p.Tipo).IsRequired();
        }
    }
}
