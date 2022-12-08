using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.DeletePricePoint.Commands
{
    public sealed record DeletePricePointCommand(string ProductId, Guid PricePointId) : IRequest<Result>;
}