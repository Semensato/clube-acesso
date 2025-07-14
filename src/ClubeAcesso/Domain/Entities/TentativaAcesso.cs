using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TentativaAcesso
    {
        public Guid Id { get; set; }
        public Guid SocioId { get; set; }
        public Guid AreaId { get; set; }
        public DateTime DataHora { get; set; }
        public ResultadoTentativa Resultado { get; set; }
    }
}
