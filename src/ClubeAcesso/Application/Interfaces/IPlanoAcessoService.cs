using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlanoAcessoService
    {
        Task<IEnumerable<PlanoAcessoDto>> ListarAsync();
        Task<PlanoAcessoDto?> ObterPorIdAsync(Guid id);
        Task CriarAsync(PlanoAcessoDto dto);
        Task AtualizarAsync(Guid id, PlanoAcessoDto dto);
        Task RemoverAsync(Guid id);
    }
}
