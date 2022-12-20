using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.DeleteProduct.Commands
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteProductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = (await _dbContext.Products.FindAsync(request.Id))!;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}