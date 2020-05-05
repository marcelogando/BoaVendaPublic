using BoaVenda.Areas.Identity.Data;
using BoaVenda.Entity.DTO;
using BoaVenda.Service;
using Entity;
using Entity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoaVenda.Controllers
{
    public class InscricaoController: Controller 
    {

        private readonly SignInManager<BoaVendaUser> _signInManager;
        private readonly UserManager<BoaVendaUser> _userManager;
        private readonly IInscricaoService _serv;

        public InscricaoController(SignInManager<BoaVendaUser> signInManager, UserManager<BoaVendaUser> userManager, IInscricaoService serv)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _serv = serv;
        }

        [HttpPost]
        public async Task<IActionResult> SetInscricao(InscricaoDTO inscricaoDTO)
        {

            var returnUrl = Url.Content("~/Dashboard/Index");
            var user = new BoaVendaUser { UserName = inscricaoDTO.Email, Email = inscricaoDTO.Email };
            var result = await _userManager.CreateAsync(user, inscricaoDTO.Senha);
            if (result.Succeeded)
            {
                UsuarioDTO usuario = new UsuarioDTO()
                {
                    Ativo = true,
                    DtCriacaoConta = DateTime.Now,
                    Email = inscricaoDTO.Email,
                    Nome = inscricaoDTO.Nome,
                    Telefone = inscricaoDTO.Telefone,
                };

                await _serv.SalvaInscricaoUsuario(usuario);

                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }

            return BadRequest("Algo errado ocorreu durante a inscrição.");
        }




    }
}
