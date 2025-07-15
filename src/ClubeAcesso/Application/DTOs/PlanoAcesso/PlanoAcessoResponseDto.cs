using Application.DTOs.AreaClube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PlanoAcesso
{
    public class PlanoAcessoResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public ICollection<AreaClubeResponseDto> Areas { get; set; } = new List<AreaClubeResponseDto>();
    }
}
