using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductMedia.Commands
{
    public sealed class SetProductMediaCommandHandler : IRequestHandler<SetProductMediaCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public SetProductMediaCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(SetProductMediaCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.ProductMedia)
                .SingleAsync();

            ProductMedia productMedia = product.SetProductMedia(request.ProductMediaId, request.MediaId);

            string userId = _authService.GetUserIdFromClaims();
            product.AddDomainEvent(new ProductModifiedEvent(product.Id, userId));

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(productMedia.Id);
        }
    }
}