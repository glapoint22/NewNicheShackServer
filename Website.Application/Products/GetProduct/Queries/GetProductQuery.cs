using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Products.GetProduct.Queries
{
    public sealed record GetProductQuery(Guid ProductId) : IRequest<Result>;
}