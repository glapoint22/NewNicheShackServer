using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Shared.EmailBuilder;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Infrastructure.Services.Common;

namespace Website.Infrastructure.Services
{
    public sealed class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(EmailMessage emailMessage)
        {
            EmailBuilder emailBuilder = new(new Repository());
            string emailBody = await emailBuilder.BuildEmail(emailMessage.EmailContent);

            emailBody = emailMessage.EmailProperties.SetEmailBody(emailBody);

            MimeMessage email = new()
            {
                Sender = MailboxAddress.Parse(_configuration["Email:Sender"]),
                Subject = emailMessage.Subject,
                Body = new TextPart(TextFormat.Html) { Text = emailBody }
            };
            email.To.Add(MailboxAddress.Parse(emailMessage.EmailAddress));


            SmtpClient smtp = new();
            await smtp.ConnectAsync(_configuration["Email:Host"], 
                Convert.ToInt32(_configuration["Email:Port"]), 
                (SecureSocketOptions)Convert.ToInt32(_configuration["Email:SecureSocketOption"]));
            await smtp.AuthenticateAsync(_configuration["Email:UserName"], _configuration["Email:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}