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
    public class SocioRepository : ISocioRepository
    {
        private readonly ClubeDbContext _context;

        public SocioRepository(ClubeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Socio entity)
        {
            await _context.Socios.AddAsync(entity);
        }

        public async Task<IEnumerable<Socio>> GetAllAsync()
        {
            return await _context.Socios.ToListAsync();
        }

        public async Task<Socio?> GetByIdAsync(Guid id)
        {
            return await _context.Socios.FindAsync(id);
        }

        public async Task<Socio?> GetByCpfAsync(string cpf)
        {
            return await _context.Socios
                .FirstOrDefaultAsync(s => s.Documento == cpf);
        }

        public void Remove(Socio entity)
        {
            _context.Socios.Remove(entity);
        }

        public void Update(Socio entity)
        {
            _context.Socios.Update(entity);
        }
    }
}
