using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoaVenda.Entity.DTO
{
    public class UsuarioDTO
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DtCriacaoConta { get; set; }
        public bool Ativo { get; set; }    
    }
}
