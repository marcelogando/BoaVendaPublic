using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoaVenda.Entity.DTO;
using BoaVenda.Repository;
using Entity.EntityFramework;

namespace BoaVenda.Service
{

    public interface IInscricaoService
    {
        Task SalvaInscricaoUsuario(UsuarioDTO usuario);
    }


    public class InscricaoService : IInscricaoService
    {

        private readonly IInscricaoRepository _rep;

        public InscricaoService(IInscricaoRepository rep)
        {
            _rep = rep;
        }


        public async Task SalvaInscricaoUsuario(UsuarioDTO usuario)
        {
            Usuario usuarioEntity = new Usuario()
            {

            };

            await _rep.SalvaInscricaoUsuario(usuarioEntity);

        }








    }
}
