using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoaVenda.Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BoaVenda.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> EnviaEmail([FromBody] ContatoDTO contato, [FromServices] AuthMessageSenderOptionsDTO sendgridConfigDTO)
        {

            if (!(contato.Nome.Equals(string.Empty) &&
                contato.Assunto.Equals(string.Empty) &&
                contato.Email.Equals(string.Empty) &&
                contato.Mensagem.Equals(string.Empty)))
            {


                var client = new SendGridClient(sendgridConfigDTO.SendGridKey);

                var recipients = new List<EmailAddress>
                {
                    new EmailAddress("marcel@mileniobus.com.br", "Marcel Ogando"),
                    new EmailAddress("lucas.simoes@mileniobus.com.br", "Lucas Simões"),
                    new EmailAddress("willian.chan@mileniobus.com.br", "Willian Chan"),
                    new EmailAddress("annaflavia.castros@hotmail.com", "Anna Flávia"),
                    new EmailAddress("lfjesus94@gmail.com", "Larissa Ferreira"),
                };


                bool displayRecipients = false;
                EmailAddress emailCliente = new EmailAddress()
                {
                    Email = contato.Email,
                    Name = contato.Nome
                };
                var msgPronta = MailHelper.CreateSingleEmailToMultipleRecipients(emailCliente, recipients, contato.Assunto, contato.Mensagem, "", displayRecipients);
                var response = await client.SendEmailAsync(msgPronta);

                return "Mensagem enviada! Entraremos em contato pelo email informado assim que possível :)";
            }
            else
            {
                return "Algo deu errado no envio da mensagem, por favor revise os campos de mensagem e tente novamente :(";
            }
        }





    }
}