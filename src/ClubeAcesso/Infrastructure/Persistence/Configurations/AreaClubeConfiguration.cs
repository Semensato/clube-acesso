using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class AreaClubeConfiguration : IEntityTypeConfiguration<AreaClube>
    {
        public void Configure(EntityTypeBuilder<AreaClube> builder)
        {
            builder.ToTable("Areas");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
