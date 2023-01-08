using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.AddSubproduct.Commands
{
    public sealed class AddSubproductCommandHandler : IRequestHandler<AddSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public AddSubproductCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(AddSubproductCommand request, CancellationToken cancellationToken)
        {
            Subproduct subproduct = Subproduct.Create(request.ProductId, request.Type);
            _dbContext.Subproducts.Add(subproduct);

            string userId = _authService.GetUserIdFromClaims();
            subproduct.AddDomainEvent(new ProductModifiedEvent(request.ProductId, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(subproduct.Id);
        }
    }
}