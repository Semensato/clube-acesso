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
    public class PlanoAcessoConfiguration : IEntityTypeConfiguration<PlanoAcesso>
    {
        public void Configure(EntityTypeBuilder<PlanoAcesso> builder)
        {
            builder.ToTable("Planos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Relacionamento muitos-para-muitos unidirecional
            builder
                .HasMany(p => p.Areas)
                .WithMany() 
                .UsingEntity<Dictionary<string, object>>(
                    "PlanoArea",
                    join => join.HasOne<AreaClube>()
                                .WithMany()
                                .HasForeignKey("AreaId")
                                .OnDelete(DeleteBehavior.Cascade),
                    join => join.HasOne<PlanoAcesso>()
                                .WithMany()
                                .HasForeignKey("PlanoId")
                                .OnDelete(DeleteBehavior.Cascade),
                    join =>
                    {
                        join.HasKey("PlanoId", "AreaId");
                        join.ToTable("PlanoArea");
                    });
        }
    }
}
