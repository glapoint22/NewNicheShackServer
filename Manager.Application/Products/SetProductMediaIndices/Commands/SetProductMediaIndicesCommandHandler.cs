using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductMediaIndices.Commands
{
    public sealed class SetProductMediaIndicesCommandHandler : IRequestHandler<SetProductMediaIndicesCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public SetProductMediaIndicesCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(SetProductMediaIndicesCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.ProductMedia)
                    .ThenInclude(x => x.Media)
                .SingleAsync();

            product.SetProductMediaIndices(request.ProductMedia);

            string userId = _authService.GetUserIdFromClaims();
            product.AddDomainEvent(new ProductModifiedEvent(product.Id, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}