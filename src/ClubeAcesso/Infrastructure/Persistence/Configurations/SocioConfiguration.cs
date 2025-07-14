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
    public class SocioConfiguration : IEntityTypeConfiguration<Socio>
    {
        public void Configure(EntityTypeBuilder<Socio> builder)
        {
            builder.ToTable("Socios");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Documento)
                .IsRequired()
                .HasMaxLength(11); // Considerando CPF

            builder.Property(s => s.PlanoId)
                .IsRequired();

            builder.HasOne(s => s.Plano)
                .WithMany() // <== sem navegação em Plano
                .HasForeignKey(s => s.PlanoId)
                .OnDelete(DeleteBehavior.Restrict); // evita cascata acidental
        }
    }
}
