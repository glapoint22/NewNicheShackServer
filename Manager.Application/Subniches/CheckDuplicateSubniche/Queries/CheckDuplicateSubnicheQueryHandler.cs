using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.CheckDuplicateSubniche.Queries
{
    public sealed class CheckDuplicateSubnicheQueryHandler : IRequestHandler<CheckDuplicateSubnicheQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public CheckDuplicateSubnicheQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(CheckDuplicateSubnicheQuery request, CancellationToken cancellationToken)
        {
            var nicheId = await _dbContext.Subniches
                .Where(x => x.Id == request.ChildId)
                .Select(x => x.NicheId)
                .SingleAsync();

            if (await _dbContext.Subniches
                .AnyAsync(x => x.NicheId == nicheId && x.Name.ToLower() == request.ChildName.Trim().ToLower()))
            {
                return Result.Succeeded(new
                {
                    id = request.ChildId,
                    name = request.ChildName,
                    parentId = nicheId
                });
            }

            return Result.Succeeded();
        }
    }
}