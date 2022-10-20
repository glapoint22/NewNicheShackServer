using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.MoveProduct.Commands
{
    public record MoveProductCommand(Guid CollaboratorProductId, string DestinationListId) : IRequest<Result>;
}