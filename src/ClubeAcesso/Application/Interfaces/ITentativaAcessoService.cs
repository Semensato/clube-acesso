using Application.DTOs.TentativaAcesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITentativaAcessoService
    {
        Task RegistrarAcessoAsync(TentativaAcessoRequestDto dto);
        Task<IEnumerable<TentativaAcessoResponseDto>> ObterPorSocioIdAsync(Guid socioId);
    }
}
