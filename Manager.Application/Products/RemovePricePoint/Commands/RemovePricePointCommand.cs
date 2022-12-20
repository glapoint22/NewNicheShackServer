using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemovePricePoint.Commands
{
    public sealed record RemovePricePointCommand(Guid ProductId, Guid PricePointId) : IRequest<Result>;
}