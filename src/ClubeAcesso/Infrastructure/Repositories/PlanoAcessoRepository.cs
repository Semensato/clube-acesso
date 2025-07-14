using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PlanoAcessoRepository : IPlanoAcessoRepository
    {
        private readonly ClubeDbContext _context;

        public PlanoAcessoRepository(ClubeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PlanoAcesso entity)
        {
            await _context.Planos.AddAsync(entity);
        }

        public async Task<IEnumerable<PlanoAcesso>> GetAllAsync(bool includeAreas = false)
        {
            if (includeAreas)
                return await _context.Planos.Include(p => p.Areas).ToListAsync();

            return await _context.Planos.ToListAsync();
        }

        public async Task<PlanoAcesso?> GetByIdAsync(Guid id, bool includeAreas = false)
        {
            if (includeAreas)
                return await _context.Planos
                    .Include(p => p.Areas)
                    .FirstOrDefaultAsync(p => p.Id == id);

            return await _context.Planos.FindAsync(id);
        }

        public async Task<PlanoAcesso?> GetByNomeAsync(string nome)
        {
            return await _context.Planos.FirstOrDefaultAsync(s => s.Nome == nome);
        }

        public void Remove(PlanoAcesso entity)
        {
            _context.Planos.Remove(entity);
        }

        public void Update(PlanoAcesso entity)
        {
            _context.Planos.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<PlanoAcesso?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlanoAcesso>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
