using Manager.Domain.Dtos;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetPricePoint.Commands
{
    public sealed record SetPricePointCommand(Guid ProductId, PricePointDto PricePoint) : IRequest<Result>;
}