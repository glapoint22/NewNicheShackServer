using Manager.Application.Common.Interfaces;
using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Products.DeleteProduct.Commands
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeleteProductCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product managerProduct = (await _managerDbContext.Products.FindAsync(request.Id))!;

            _managerDbContext.Products.Remove(managerProduct);
            await _managerDbContext.SaveChangesAsync();



            Website.Domain.Entities.Product websiteProduct = (await _websiteDbContext.Products.FindAsync(request.Id))!;

            _websiteDbContext.Products.Remove(websiteProduct);
            await _websiteDbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}