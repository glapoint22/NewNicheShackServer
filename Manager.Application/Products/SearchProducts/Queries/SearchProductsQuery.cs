using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SearchProducts.Queries
{
    public sealed record SearchProductsQuery(string SearchTerm) : IRequest<Result>;
}