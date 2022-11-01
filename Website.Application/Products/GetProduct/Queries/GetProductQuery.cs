using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Products.GetProduct.Queries
{
    public sealed record GetProductQuery(int ProductId) : IRequest<Result>;
}