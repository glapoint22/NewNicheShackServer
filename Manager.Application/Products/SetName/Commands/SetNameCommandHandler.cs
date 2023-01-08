using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetName.Commands
{
    public sealed class SetNameCommandHandler : IRequestHandler<SetNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public SetNameCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(SetNameCommand request, CancellationToken cancellationToken)
        {
            Product product = (await _dbContext.Products.FindAsync(request.Id))!;
            product.SetName(request.Name);

            string userId = _authService.GetUserIdFromClaims();
            product.AddDomainEvent(new ProductModifiedEvent(product.Id, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}