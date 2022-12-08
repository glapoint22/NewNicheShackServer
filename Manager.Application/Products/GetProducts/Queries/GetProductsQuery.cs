using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.GetProducts.Queries
{
    public sealed record GetProductsQuery(string ParentId) : IRequest<Result>;
}