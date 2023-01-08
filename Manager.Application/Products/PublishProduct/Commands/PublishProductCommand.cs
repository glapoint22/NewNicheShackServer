using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.PublishProduct.Commands
{
    public sealed record PublishProductCommand(Guid ProductId) : IRequest<Result>;
}