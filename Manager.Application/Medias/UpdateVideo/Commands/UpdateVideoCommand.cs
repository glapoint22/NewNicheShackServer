using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Medias.UpdateVideo.Commands
{
    public sealed record UpdateVideoCommand(Guid Id, string? VideoId, int VideoType, string Name) : IRequest<Result>;
}