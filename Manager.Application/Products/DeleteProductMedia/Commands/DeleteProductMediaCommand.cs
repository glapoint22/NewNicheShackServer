using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.DeleteProductMedia.Commands
{
    public sealed record DeleteProductMediaCommand(Guid Id) : IRequest<Result>;
}