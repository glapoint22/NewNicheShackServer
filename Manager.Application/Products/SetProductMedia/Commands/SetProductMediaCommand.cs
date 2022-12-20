using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductMedia.Commands
{
    public sealed record SetProductMediaCommand(Guid ProductId, Guid? ProductMediaId, Guid MediaId) : IRequest<Result>;
}