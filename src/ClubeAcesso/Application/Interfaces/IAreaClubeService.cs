using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAreaClubeService
    {
        Task<IEnumerable<AreaClubeDto>> ListarAsync();
        Task<AreaClubeDto?> ObterPorIdAsync(Guid id);
        Task CriarAsync(AreaClubeDto dto);
        Task AtualizarAsync(Guid id, AreaClubeDto dto);
        Task RemoverAsync(Guid id);
    }
}
