using Manager.Domain.Dtos;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdatePricePoint.Commands
{
    public sealed record UpdatePricePointCommand(string ProductId, PricePointDto PricePoint) : IRequest<Result>;
}