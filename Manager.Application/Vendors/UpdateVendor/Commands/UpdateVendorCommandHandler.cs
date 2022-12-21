using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.UpdateVendor.Commands
{
    public sealed class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateVendorCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            Vendor vendor = (await _dbContext.Vendors.FindAsync(request.Id))!;
            vendor.Update(request.Name, request.PrimaryEmail, request.PrimaryFirstName, request.PrimaryLastName);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}