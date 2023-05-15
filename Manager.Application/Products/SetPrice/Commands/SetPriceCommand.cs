using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetPrice.Commands
{
    public sealed record SetPriceCommand(Guid ProductId, double Price, string Currency) : IRequest<Result>;
}