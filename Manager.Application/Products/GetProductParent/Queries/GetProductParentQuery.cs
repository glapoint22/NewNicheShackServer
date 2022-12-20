using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.GetProductParent.Queries
{
    public sealed record GetProductParentQuery(Guid ProductId) : IRequest<Result>;
}