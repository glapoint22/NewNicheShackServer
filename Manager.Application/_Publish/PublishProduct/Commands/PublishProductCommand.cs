using MediatR;
using Shared.Common.Classes;

namespace Manager.Application._Publish.PublishProduct.Commands
{
    public sealed record PublishProductCommand(Guid ProductId) : IRequest<Result>;
}