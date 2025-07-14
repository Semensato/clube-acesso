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
    public class TentativaAcessoRepository : ITentativaAcessoRepository
    {
        private readonly ClubeDbContext _context;

        public TentativaAcessoRepository(ClubeDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TentativaAcesso entity)
        {
            await _context.Tentativas.AddAsync(entity);
        }

        public async Task<IEnumerable<TentativaAcesso>> GetAllAsync()
        {
            return await _context.Tentativas.ToListAsync();
        }

        public async Task<TentativaAcesso?> GetByIdAsync(Guid id)
        {
            return await _context.Tentativas.FindAsync(id);
        }

        public async Task<IEnumerable<TentativaAcesso>> GetBySocioIdAsync(Guid socioId)
        {
            return await _context.Tentativas.Where(t => t.SocioId == socioId).ToListAsync();
        }

        public void Remove(TentativaAcesso entity)
        {
            _context.Tentativas.Remove(entity);
        }

        public void Update(TentativaAcesso entity)
        {
            _context.Tentativas.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
