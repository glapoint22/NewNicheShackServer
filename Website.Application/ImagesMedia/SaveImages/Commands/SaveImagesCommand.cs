using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common.Classes;

namespace Website.Application.ImagesMedia.SaveImages.Commands
{
    public sealed record SaveImagesCommand(IFormCollection Images) : IRequest<Result>;
}