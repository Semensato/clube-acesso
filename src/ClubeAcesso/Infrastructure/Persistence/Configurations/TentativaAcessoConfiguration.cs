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
    public class TentativaAcessoConfiguration : IEntityTypeConfiguration<TentativaAcesso>
    {
        public void Configure(EntityTypeBuilder<TentativaAcesso> builder)
        {
            builder.ToTable("TentativasAcesso");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.SocioId)
                .IsRequired();

            builder.Property(t => t.AreaId)
                .IsRequired();

            builder.Property(t => t.DataHora)
                .IsRequired();

            builder.Property(t => t.Resultado)
                .IsRequired()
                .HasConversion<int>(); // armazena enum como inteiro
        }
    }
}
