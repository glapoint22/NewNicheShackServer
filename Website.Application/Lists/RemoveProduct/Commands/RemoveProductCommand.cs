using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Lists.RemoveProduct.Commands
{
    public sealed record RemoveProductCommand(Guid ProductId, string ListId) : IRequest<Result>;
}