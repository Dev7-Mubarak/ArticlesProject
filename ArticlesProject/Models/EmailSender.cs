using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ArticlesProject.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSetting _smtpSettings;

        public EmailSender(IOptions<SmtpSetting> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using var client = new SmtpClient
            {
                Port = _smtpSettings.Port,
                Host = _smtpSettings.Host,
                EnableSsl = true,
                Credentials = new NetworkCredential(_smtpSettings.From, _smtpSettings.Password),
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.From),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}
