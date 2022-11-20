using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Products.GetProperties.Queries
{
    public sealed record GetPropertiesQuery(string ProductId) : IRequest<Result>;
}