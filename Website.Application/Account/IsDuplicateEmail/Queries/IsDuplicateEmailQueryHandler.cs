using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;

namespace Website.Application.Account.IsDuplicateEmail.Queries
{
    public class IsDuplicateEmailQueryHandler : IRequestHandler<IsDuplicateEmailQuery, bool>
    {
        private readonly IWebsiteDbContext _dbContext;

        public IsDuplicateEmailQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(IsDuplicateEmailQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email == request.Email);
        }
    }
}