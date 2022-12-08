using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetShipping.Commands
{
    public sealed record SetShippingCommand(string Id, int ShippingType) : IRequest<Result>;
}