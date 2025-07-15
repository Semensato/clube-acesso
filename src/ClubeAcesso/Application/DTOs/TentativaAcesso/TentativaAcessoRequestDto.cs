using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TentativaAcesso
{
    public class TentativaAcessoRequestDto
    {
        public Guid SocioId { get; set; }
        public Guid AreaId { get; set; }
    }
}
