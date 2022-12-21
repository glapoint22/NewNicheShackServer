using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.AddVendor.Commands
{
    public sealed class AddVendorCommandHandler : IRequestHandler<AddVendorCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddVendorCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddVendorCommand request, CancellationToken cancellationToken)
        {
            Vendor vendor = Vendor.Create(request.Name, request.PrimaryEmail, request.PrimaryFirstName, request.PrimaryLastName);
            _dbContext.Vendors.Add(vendor);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(vendor.Id);
        }
    }
}