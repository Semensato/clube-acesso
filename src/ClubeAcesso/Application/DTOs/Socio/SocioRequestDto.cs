using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Socio
{
    public class SocioRequestDto
    {
        public string Nome { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public Guid PlanoId { get; set; }
    }
}
