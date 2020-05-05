using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoaVenda.Models;
using Entity.EntityFramework;


namespace BoaVenda.Repository
{

    public interface IInscricaoRepository
    {
        Task SalvaInscricaoUsuario(Usuario usuarioEntity);
    }


    public class InscricaoRepository : IInscricaoRepository
    {

        private readonly BoaVendaContext _ctx;

        public InscricaoRepository(BoaVendaContext ctx)
        {
            _ctx = ctx;
        }


        public async Task SalvaInscricaoUsuario(Usuario usuarioEntity)
        {
            _ctx.Usuarios.Add(usuarioEntity);
            await _ctx.SaveChangesAsync();
        }







    }
}
