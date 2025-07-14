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
    public class AreaClubeRepository : IAreaClubeRepository
    {
        private readonly ClubeDbContext _context;

        public AreaClubeRepository(ClubeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AreaClube entity)
        {
            await _context.Areas.AddAsync(entity);
        }

        public async Task<IEnumerable<AreaClube>> GetAllAsync()
        {
            return await _context.Areas.ToListAsync();
        }

        public async Task<AreaClube?> GetByIdAsync(Guid id)
        {
            return await _context.Areas.FindAsync(id);
        }

        public async Task<AreaClube?> GetByNomeAsync(string nome)
        {
            return await _context.Areas.FirstOrDefaultAsync(s => s.Nome == nome);
        }

        public void Remove(AreaClube entity)
        {
            _context.Areas.Remove(entity);
        }

        public void Update(AreaClube entity)
        {
            _context.Areas.Update(entity);
        }
    }
}
