using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ClubeDbContext : DbContext
    {
        public ClubeDbContext(DbContextOptions<ClubeDbContext> options) : base(options) { }

        public DbSet<Socio> Socios { get; set; }
        public DbSet<PlanoAcesso> Planos { get; set; }
        public DbSet<AreaClube> Areas { get; set; }
        public DbSet<TentativaAcesso> Tentativas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClubeDbContext).Assembly);
        }
    }
}
