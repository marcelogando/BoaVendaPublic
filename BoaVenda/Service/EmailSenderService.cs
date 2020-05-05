using BoaVenda.Entity.DTO;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BoaVenda.Service
{
    public interface IEmailSenderService
    {
        AuthMessageSenderOptionsDTO Options { get; }
        Task Execute(string apiKey, string subject, string message, string email, string fromEmail, string fromName);
        Task SendEmailAsync(string email, string subject, string message, string fromEmail, string fromName);
    }

    public class EmailSenderService : IEmailSenderService
    {

        public EmailSenderService(IOptions<AuthMessageSenderOptionsDTO> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptionsDTO Options { get; } //PEGA DO APPSETTINGS MESMO E QUE SE DANE :p

        public Task SendEmailAsync(string email, string subject, string message, string fromEmail, string fromName)
        {
            return Execute(Options.SendGridKey, subject, message, email, fromEmail, fromName);
        }

        public Task Execute(string apiKey, string subject, string message, string email, string fromEmail, string fromName)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
