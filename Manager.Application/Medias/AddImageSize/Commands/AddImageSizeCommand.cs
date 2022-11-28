using MediatR;
using Shared.Common.Classes;
using Shared.Common.Enums;

namespace Manager.Application.Medias.AddImageSize.Commands
{
    public sealed record AddImageSizeCommand(Guid ImageId, ImageSizeType ImageSizeType, string ImageSrc) : IRequest<Result>;
}