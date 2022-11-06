using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.RemoveProduct.Commands
{
    public sealed record RemoveProductCommand(string ProductId, string ListId) : IRequest<Result>;
}