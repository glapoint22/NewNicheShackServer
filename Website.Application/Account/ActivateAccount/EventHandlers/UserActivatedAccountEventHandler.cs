﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.EmailBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.ActivateAccount.EventHandlers
{
    public sealed class UserActivatedAccountEventHandler : INotificationHandler<UserActivatedAccountEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IWebsiteDbContext _dbContext;

        public UserActivatedAccountEventHandler(IEmailService emailService, IWebsiteDbContext dbContext)
        {
            _emailService = emailService;
            _dbContext = dbContext;
        }


        public async Task Handle(UserActivatedAccountEvent notification, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Where(x => x.Id == notification.UserId)
                .Select(x => new { 
                    x.FirstName,
                    x.LastName,
                    x.Email
                }).SingleAsync();

            // Get the email from the database
            string emailContent = await _dbContext.Emails
                .Where(x => x.Name == "Welcome To Niche Shack")
                .Select(x => x.Content)
                .SingleAsync();


            // Get the email body
            string emailBody = await _emailService.GetEmailBody(emailContent);


            // Create the email message
            EmailMessage emailMessage = new(emailBody, user.Email, "Welcome To Niche Shack", new()
            {
                Recipient = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                },
            });

            // Send the email
            await _emailService.SendEmail(emailMessage);
        }
    }
}