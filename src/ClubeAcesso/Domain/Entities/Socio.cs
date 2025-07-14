using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Socio
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public Guid PlanoId { get; set; }
        public PlanoAcesso Plano { get; set; } = null!;
    }
}
