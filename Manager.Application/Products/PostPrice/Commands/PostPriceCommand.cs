using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.PostPrice.Commands
{
    public sealed record PostPriceCommand(string ProductId, double Price) : IRequest<Result>;
}