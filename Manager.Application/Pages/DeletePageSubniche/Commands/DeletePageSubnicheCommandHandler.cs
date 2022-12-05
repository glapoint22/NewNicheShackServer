using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePageSubniche.Commands
{
    public sealed class DeletePageSubnicheCommandHandler : IRequestHandler<DeletePageSubnicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeletePageSubnicheCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeletePageSubnicheCommand request, CancellationToken cancellationToken)
        {
            PageSubniche pageSubniche = await _dbContext.PageSubniches
                .Where(x => x.PageId == request.PageId && x.SubnicheId == request.SubnicheId)
                .SingleAsync();

            _dbContext.PageSubniches.Remove(pageSubniche);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}