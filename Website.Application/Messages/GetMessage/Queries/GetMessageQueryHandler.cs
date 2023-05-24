using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Messages.GetMessage.Queries
{
    public sealed class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetMessageQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            string? text = await _dbContext.Messages.Select(x => x.Text).SingleOrDefaultAsync();

            return Result.Succeeded(new
            {
                Text = text
            });
        }
    }
}