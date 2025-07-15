using Application.DTOs.PlanoAcesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Socio
{
    public class SocioResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public PlanoAcessoResponseDto Plano { get; set; } = null!;
    }
}
