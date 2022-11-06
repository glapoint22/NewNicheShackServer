using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.MoveProduct.Commands
{
    public sealed record MoveProductCommand(string ProductId, string SourceListId, string DestinationListId) : IRequest<Result>;
}