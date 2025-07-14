using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISocioService
    {
        Task<IEnumerable<SocioDto>> ListarAsync();
        Task<SocioDto?> ObterPorIdAsync(Guid id);
        Task<SocioDto?> ObterPorCpfAsync(string cpf);
        Task CriarAsync(SocioDto dto);
        Task AtualizarAsync(Guid id, SocioDto dto);
        Task RemoverAsync(Guid id);
    }
}
