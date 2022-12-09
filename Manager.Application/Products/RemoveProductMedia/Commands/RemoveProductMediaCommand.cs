using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemoveProductMedia.Commands
{
    public sealed record RemoveProductMediaCommand(string ProductId, Guid ProductMediaId) : IRequest<Result>;
}