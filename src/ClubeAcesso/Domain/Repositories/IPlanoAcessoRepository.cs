using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPlanoAcessoRepository : IRepository<PlanoAcesso>
    {
        Task<PlanoAcesso?> GetByNomeAsync(string nome);
        public Task<IEnumerable<PlanoAcesso>> GetAllAsync(bool includeAreas = false);
        public Task<PlanoAcesso?> GetByIdAsync(Guid id, bool includeAreas = false);
    }
}
