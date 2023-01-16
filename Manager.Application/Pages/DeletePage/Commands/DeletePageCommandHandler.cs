using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Pages.DeletePage.Commands
{
    public sealed class DeletePageCommandHandler : IRequestHandler<DeletePageCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeletePageCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            Page Page = await _dbContext.Pages
                .Where(x => x.Id == request.PageId)
                .SingleAsync();

            _dbContext.Pages.Remove(Page);
            await _dbContext.SaveChangesAsync();


            Website.Domain.Entities.Page? websitePage = await _websiteDbContext.Pages.FindAsync(request.PageId);

            if (websitePage != null)
            {
                _websiteDbContext.Pages.Remove(websitePage);
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}