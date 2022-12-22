using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.CheckDuplicateVendor.Queries
{
    public sealed class CheckDuplicateVendorQueryHandler : IRequestHandler<CheckDuplicateVendorQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public CheckDuplicateVendorQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(CheckDuplicateVendorQuery request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Vendors.AnyAsync(x => x.Name.ToLower() == request.VendorName.ToLower()))
                return Result.Succeeded(new
                {
                    Name = request.VendorName
                });

            return Result.Succeeded();
        }
    }
}