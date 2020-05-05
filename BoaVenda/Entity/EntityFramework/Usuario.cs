using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.EntityFramework
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DtCriacaoConta { get; set; }
        public bool Ativo { get; set; }
    }
}
