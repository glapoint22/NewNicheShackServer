using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemoveProductMedia.Commands
{
    public sealed record RemoveProductMediaCommand(Guid ProductId, Guid ProductMediaId) : IRequest<Result>;
}