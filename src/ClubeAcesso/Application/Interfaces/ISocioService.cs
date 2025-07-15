using Application.DTOs.Socio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISocioService
    {
        Task<IEnumerable<SocioResponseDto>> ListarAsync();
        Task<SocioResponseDto?> ObterPorIdAsync(Guid id);
        Task<SocioResponseDto?> ObterPorCpfAsync(string cpf);
        Task<SocioResponseDto?> CriarAsync(SocioRequestDto dto);
        Task AtualizarAsync(Guid id, SocioRequestDto dto);
        Task RemoverAsync(Guid id);
    }
}
