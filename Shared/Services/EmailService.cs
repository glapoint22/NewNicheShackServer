using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Shared.EmailBuilder.Classes;


namespace Shared.Services
{
    public abstract class EmailService
    {
        protected IConfiguration _configuration = null!;
        protected EmailBuilder.EmailBuilder _emailBuilder = null!;


        // ---------------------------------------------------------------------------- Get Email Body ---------------------------------------------------------------------------
        public async Task<string> GetEmailBody(string emailContent)
        {
            return await _emailBuilder.BuildEmail(emailContent);
        }







        // ------------------------------------------------------------------------------ Send Email -----------------------------------------------------------------------------
        public async Task SendEmail(EmailMessage emailMessage)
        {
            emailMessage.EmailProperties.Host = _configuration["Email:WebHost"];
            emailMessage.EmailProperties.Recipient.Email = emailMessage.EmailAddress;
            emailMessage.EmailBody = emailMessage.EmailProperties.SetEmailBody(emailMessage.EmailBody);

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
    }
}