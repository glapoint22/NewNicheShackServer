using Manager.Application.Common.Interfaces;
using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Niches.DeleteNiche.Commands
{
    public sealed class DeleteNicheCommandHandler : IRequestHandler<DeleteNicheCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeleteNicheCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeleteNicheCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Niche managerNiche = (await _managerDbContext.Niches.FindAsync(request.Id))!;

            _managerDbContext.Niches.Remove(managerNiche);
            await _managerDbContext.SaveChangesAsync();


            Website.Domain.Entities.Niche? websiteNiche = await _websiteDbContext.Niches.FindAsync(request.Id);

            if (websiteNiche != null)
            {
                _websiteDbContext.Niches.Remove(websiteNiche);
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}