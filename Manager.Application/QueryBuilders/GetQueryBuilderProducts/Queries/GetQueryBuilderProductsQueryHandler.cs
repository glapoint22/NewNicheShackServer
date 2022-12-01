using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Dtos;
using Shared.QueryBuilder.Classes;
using System.Text.Json;

namespace Manager.Application.QueryBuilders.GetQueryBuilderProducts.Queries
{
    public sealed class GetQueryBuilderProductsQueryHandler : IRequestHandler<GetQueryBuilderProductsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetQueryBuilderProductsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetQueryBuilderProductsQuery request, CancellationToken cancellationToken)
        {
            Query query = JsonSerializer.Deserialize<Query>(request.QueryString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;


            QueryBuilder queryBuilder = new QueryBuilder();
            var queryExpression = queryBuilder.BuildQuery<Product>(query);

            var products = await _dbContext.Products
                .Where(queryExpression)
                .Take(24)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlName = x.UrlName,
                    MinPrice = x.ProductPrices.MinPrice(),
                    MaxPrice = x.ProductPrices.MaxPrice(),
                    Image = new PageImage
                    {
                        Name = x.Media.Name,
                        Src = x.Media.ImageSm!
                    }
                }).ToListAsync();

            return Result.Succeeded(products);
        }
    }
}