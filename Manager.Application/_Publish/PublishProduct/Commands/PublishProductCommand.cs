using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application._Publish.PublishProduct.Commands
{
    public sealed record PublishProductCommand(Product Product) : IRequest<Result>;
}