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
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Cpf).HasMaxLength(14).IsRequired();

            builder.Property(c => c.Active).HasDefaultValue(true);
            builder.Property(c => c.AddressId).IsRequired();
            builder.Property(c => c.Birth).IsRequired();

            builder.HasKey(c => c.Id);
        }
    }
}
