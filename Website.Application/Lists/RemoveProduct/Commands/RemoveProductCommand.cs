using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.RemoveProduct.Commands
{
    public record RemoveProductCommand(Guid CollaboratorProductId) : IRequest<Result>;
}