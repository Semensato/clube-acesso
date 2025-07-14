using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ISocioRepository : IRepository<Socio>
    {
        Task<Socio?> GetByCpfAsync(string cpf);
    }
}
