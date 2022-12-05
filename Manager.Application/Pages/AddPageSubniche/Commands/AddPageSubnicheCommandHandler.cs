using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.AddPageSubniche.Commands
{
    public sealed class AddPageSubnicheCommandHandler : IRequestHandler<AddPageSubnicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddPageSubnicheCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddPageSubnicheCommand request, CancellationToken cancellationToken)
        {
            Page page = await _dbContext.Pages
                .Where(x => x.Id == request.PageId)
                .Include(x => x.PageSubniches)
                .SingleAsync();

            page.AddPageSubniche(request.SubnicheId);

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}