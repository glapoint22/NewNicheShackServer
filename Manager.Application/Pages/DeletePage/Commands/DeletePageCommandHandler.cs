using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePage.Commands
{
    public sealed class DeletePageCommandHandler : IRequestHandler<DeletePageCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeletePageCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            Page Page = await _dbContext.Pages
                .Where(x => x.Id == request.PageId)
                .SingleAsync();

            _dbContext.Pages.Remove(Page);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}