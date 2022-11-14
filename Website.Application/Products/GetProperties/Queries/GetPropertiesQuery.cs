using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Products.GetProperties.Queries
{
    public sealed record GetPropertiesQuery(string ProductId) : IRequest<Result>;
}