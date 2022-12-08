using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.GetProduct.Queries
{
    public sealed record GetProductQuery(string ProductId) : IRequest<Result>;
}