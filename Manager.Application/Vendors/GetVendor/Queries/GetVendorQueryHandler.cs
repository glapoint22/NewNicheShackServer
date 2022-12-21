using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.GetVendor.Queries
{
    public sealed class GetVendorQueryHandler : IRequestHandler<GetVendorQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetVendorQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetVendorQuery request, CancellationToken cancellationToken)
        {
            var vendor = await _dbContext.Vendors
                .Where(x => x.Id == request.Id)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.PrimaryEmail,
                    x.PrimaryFirstName,
                    x.PrimaryLastName
                }).SingleAsync();

            return Result.Succeeded(vendor);
        }
    }
}