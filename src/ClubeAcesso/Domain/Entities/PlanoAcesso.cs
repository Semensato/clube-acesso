using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlanoAcesso
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public ICollection<AreaClube> Areas { get; set; } = null!;
    }
}
