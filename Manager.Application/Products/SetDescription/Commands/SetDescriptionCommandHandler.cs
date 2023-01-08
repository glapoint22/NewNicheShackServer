using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetDescription.Commands
{
    public sealed class SetDescriptionCommandHandler : IRequestHandler<SetDescriptionCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public SetDescriptionCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(SetDescriptionCommand request, CancellationToken cancellationToken)
        {
            Product product = (await _dbContext.Products.FindAsync(request.Id))!;
            product.SetDescription(request.Description);

            string userId = _authService.GetUserIdFromClaims();
            product.AddDomainEvent(new ProductModifiedEvent(product.Id, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}