using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetSubproduct.Commands
{
    public sealed record SetSubproductCommandHandler : IRequestHandler<SetSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public SetSubproductCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(SetSubproductCommand request, CancellationToken cancellationToken)
        {
            Subproduct subproduct = (await _dbContext.Subproducts.FindAsync(request.SubproductId))!;
            subproduct.Set(request.Name, request.Description, request.ImageId, request.Value);

            string userId = _authService.GetUserIdFromClaims();
            subproduct.AddDomainEvent(new ProductModifiedEvent(subproduct.ProductId, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}