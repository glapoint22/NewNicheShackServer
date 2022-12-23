using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

namespace Manager.Application.Emails.SearchEmails.Queries
{
    public sealed class SearchEmailsQueryHandler : IRequestHandler<SearchEmailsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchEmailsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchEmailsQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new();

            var query = queryBuilder.BuildQuery<Email>(request.SearchTerm);
            var emails = await _dbContext.Emails
                .Where(query)
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(emails);
        }
    }
}