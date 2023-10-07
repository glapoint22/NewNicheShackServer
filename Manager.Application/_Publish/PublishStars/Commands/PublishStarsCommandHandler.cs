using Manager.Application._Publish.Common.Classes;
using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application._Publish.PublishStars.Commands
{
    public sealed class PublishStarsCommandHandler : Publish, IRequestHandler<PublishStarsCommand, Result>
    {
        public PublishStarsCommandHandler(
            IWebsiteDbContext websiteDbContext,
            IManagerDbContext managerDbContext,
            Application.Common.Interfaces.IMediaService mediaService,
            Application.Common.Interfaces.IAuthService authService,
            IConfiguration configuration) : base(websiteDbContext, managerDbContext, mediaService, authService, configuration)
        {

        }

        public async Task<Result> Handle(PublishStarsCommand request, CancellationToken cancellationToken)
        {
            List<Guid> mediaIds = await _managerDbContext.Media
                .Where(x => x.Name == "One Star" ||
                    x.Name == "Two Stars" ||
                    x.Name == "Three Stars" ||
                    x.Name == "Four Stars" ||
                    x.Name == "Five Stars")
                .Select(x => x.Id)
                .ToListAsync();

            await PostImages(mediaIds);

            await _websiteDbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}