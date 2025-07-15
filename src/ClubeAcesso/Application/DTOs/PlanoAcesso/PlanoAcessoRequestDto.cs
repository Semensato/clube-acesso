using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PlanoAcesso
{
    public class PlanoAcessoRequestDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public ICollection<Guid> Areas { get; set; } = new List<Guid>();
    }
}
