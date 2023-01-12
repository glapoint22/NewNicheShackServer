using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.GetDisabledProducts.Queries
{
    public sealed record GetDisabledProductsQuery() : IRequest<Result>;
}