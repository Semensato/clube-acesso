using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SocioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public PlanoAcessoDto PlanoAcesso { get; set; } = null!;
    }
}
