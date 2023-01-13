using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.DisableEnableProduct.Commands
{
    public sealed record DisableEnableProductCommand(Guid ProductId) : IRequest<Result>;
}