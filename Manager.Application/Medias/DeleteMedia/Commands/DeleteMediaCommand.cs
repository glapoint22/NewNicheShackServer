using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Medias.DeleteMedia.Commands
{
    public sealed record DeleteMediaCommand(Guid Id) : IRequest<Result>;
}