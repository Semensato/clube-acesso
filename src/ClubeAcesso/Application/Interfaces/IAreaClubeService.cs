using Application.DTOs.AreaClube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAreaClubeService
    {
        Task<IEnumerable<AreaClubeResponseDto>> ListarAsync();
        Task<AreaClubeResponseDto?> ObterPorIdAsync(Guid id);
        Task<AreaClubeResponseDto?> CriarAsync(AreaClubeRequestDto dto);
        Task AtualizarAsync(Guid id, AreaClubeRequestDto dto);
        Task RemoverAsync(Guid id);
    }
}
