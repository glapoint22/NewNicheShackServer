using MediatR;
using Shared.Common.Classes;
using Shared.Common.Enums;

namespace Manager.Application.Medias.PostVideo.Commands
{
    public sealed record PostVideoCommand(string Name, string VideoId, VideoType VideoType) : IRequest<Result>;
}