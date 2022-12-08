using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetMedia.Commands
{
    public sealed record SetMediaCommand(string ProductId, Guid? ProductMediaId, Guid MediaId) : IRequest<Result>;
}