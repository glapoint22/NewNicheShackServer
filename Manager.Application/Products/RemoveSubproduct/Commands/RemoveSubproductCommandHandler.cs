using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemoveSubproduct.Commands
{
    public sealed class RemoveSubproductCommandHandler : IRequestHandler<RemoveSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public RemoveSubproductCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(RemoveSubproductCommand request, CancellationToken cancellationToken)
        {
            Subproduct subproduct = (await _dbContext.Subproducts.FindAsync(request.SubproductId))!;
            _dbContext.Subproducts.Remove(subproduct);

            string userId = _authService.GetUserIdFromClaims();
            subproduct.AddDomainEvent(new ProductModifiedEvent(subproduct.ProductId, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}