using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.DeleteVendor.Commands
{
    public sealed class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteVendorCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            Vendor vendor = (await _dbContext.Vendors.FindAsync(request.Id))!;

            _dbContext.Vendors.Remove(vendor);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}