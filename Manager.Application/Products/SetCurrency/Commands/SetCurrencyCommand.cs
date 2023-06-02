using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetCurrency.Commands
{
    public sealed record SetCurrencyCommand(Guid ProductId, string Currency) : IRequest<Result>;
}