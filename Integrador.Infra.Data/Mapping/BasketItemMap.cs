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
    public class BasketItemMap : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.ToTable("BasketItem");
            builder.Property(bi => bi.ProductId).IsRequired();
            builder.Property(bi => bi.Amount).IsRequired();

            builder.HasOne(bi => bi.Basket)
                .WithMany(bi => bi.Items)
                .HasForeignKey(bi => bi.BasketId)
                .HasConstraintName("Fk_BasketItem_Basket")
                .IsRequired();
        }
    }
}
