using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdatePrice.Commands
{
    public sealed record UpdatePriceCommand(string ProductId, double Price) : IRequest<Result>;
}