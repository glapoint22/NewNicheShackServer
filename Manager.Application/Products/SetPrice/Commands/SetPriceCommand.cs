using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetPrice.Commands
{
    public sealed record SetPriceCommand(string ProductId, double Price) : IRequest<Result>;
}