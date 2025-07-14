using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PlanoAcessoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public ICollection<AreaClubeDto> Areas { get; set; } = null!;
    }
}
