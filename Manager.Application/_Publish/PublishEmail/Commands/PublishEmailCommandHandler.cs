using Manager.Application._Publish.Common.Classes;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application._Publish.PublishEmail.Commands
{
    public sealed class PublishEmailCommandHandler : Publish, IRequestHandler<PublishEmailCommand, Result>
    {
        public PublishEmailCommandHandler(
            IWebsiteDbContext websiteDbContext,
            IManagerDbContext managerDbContext,
            Application.Common.Interfaces.IMediaService mediaService,
            Application.Common.Interfaces.IAuthService authService,
            IConfiguration configuration) : base(websiteDbContext, managerDbContext, mediaService, authService, configuration)
        {
        }

        public async Task<Result> Handle(PublishEmailCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.PublishItem? publishItem = await _managerDbContext.PublishItems
                .Where(x => x.EmailId == request.EmailId)
                .SingleOrDefaultAsync();

            if (publishItem != null)
            {
                Domain.Entities.Email email = await _managerDbContext.Emails
                    .Where(x => x.Id == request.EmailId)
                    .SingleAsync();

                await PublishMedia(email);


                if (publishItem.PublishStatus == PublishStatus.New)
                {
                    // Add the email to website
                    Website.Domain.Entities.Email websiteEmail = new()
                    {
                        Id = email.Id,
                        Type = email.Type,
                        Name = email.Name,
                        Content = email.Content
                    };

                    _websiteDbContext.Emails.Add(websiteEmail);
                }
                else
                {
                    Website.Domain.Entities.Email websiteEmail = await _websiteDbContext.Emails
                        .Where(x => x.Id == email.Id)
                        .SingleAsync();

                    websiteEmail.Type = email.Type;
                    websiteEmail.Name = email.Name;
                    websiteEmail.Content = email.Content;
                }


                // Remove the publish item
                _managerDbContext.PublishItems.Remove(publishItem);
                await _managerDbContext.SaveChangesAsync();

                // Upadate the website
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }





        // ------------------------------------------------------------------------------ Publish Media ---------------------------------------------------------------------------
        private async Task PublishMedia(Domain.Entities.Email email)
        {
            // Get the media ids from website that this page is using
            List<Guid> websiteMediaIds = new();


            Website.Domain.Entities.Email? websiteEmail = await _websiteDbContext.Emails
                .Where(x => x.Id == email.Id)
                .SingleOrDefaultAsync();

            if (websiteEmail != null)
            {
                websiteMediaIds = GetMediaIds(websiteEmail.Content);
            }


            // Get the media ids from manager that website does not have
            List<Guid> managerMediaIds = GetMediaIds(email.Content)
                .Where(x => !websiteMediaIds
                    .Contains(x))
                .ToList();



            // If we have media ids from manager that website does not have
            if (managerMediaIds.Count > 0)
            {
                await PostImages(managerMediaIds);
            }
        }
    }
}