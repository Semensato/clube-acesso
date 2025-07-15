using Application.DTOs.PlanoAcesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlanoAcessoService
    {
        Task<IEnumerable<PlanoAcessoResponseDto>> ListarAsync();
        Task<PlanoAcessoResponseDto?> ObterPorIdAsync(Guid id);
        Task CriarAsync(PlanoAcessoRequestDto dto);
        Task AtualizarAsync(Guid id, PlanoAcessoRequestDto dto);
        Task RemoverAsync(Guid id);
    }
}
