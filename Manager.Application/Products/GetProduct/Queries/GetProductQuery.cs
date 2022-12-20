using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.GetProduct.Queries
{
    public sealed record GetProductQuery(Guid ProductId) : IRequest<Result>;
}