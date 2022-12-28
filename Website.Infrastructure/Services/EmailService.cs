using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly EmailBuilder _emailBuilder;

        public EmailService(IConfiguration configuration, IWebsiteDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _emailBuilder = new(new Repository(dbContext));
        }



        // ---------------------------------------------------------------------------- Get Email Body ---------------------------------------------------------------------------
        public async Task<string> GetEmailBody(string emailContent)
        {
            return await _emailBuilder.BuildEmail(emailContent);
        }







        // ------------------------------------------------------------------------------ Send Email -----------------------------------------------------------------------------
        public async Task SendEmail(EmailMessage emailMessage)
        {
            MimeMessage email = new()
            {
                Sender = MailboxAddress.Parse(_configuration["Email:Sender"]),
                Subject = emailMessage.Subject,
                Body = new TextPart(TextFormat.Html) { Text = emailMessage.EmailBody }
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







        // ------------------------------------------------------------------------------- Get Host ------------------------------------------------------------------------------
        public string GetHost()
        {
            if (_webHostEnvironment.IsDevelopment())
            {
                return "http://localhost:4200";
            }

            return "https://www.nicheshack.com";
        }
    }
}