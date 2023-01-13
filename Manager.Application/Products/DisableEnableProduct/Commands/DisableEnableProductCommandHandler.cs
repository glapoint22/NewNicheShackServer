﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Products.DisableEnableProduct.Commands
{
    public sealed class DisableEnableProductCommandHandler : IRequestHandler<DisableEnableProductCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public DisableEnableProductCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DisableEnableProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products.Where(x => x.Id == request.ProductId).SingleAsync();

            product.Disabled = !product.Disabled;

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}