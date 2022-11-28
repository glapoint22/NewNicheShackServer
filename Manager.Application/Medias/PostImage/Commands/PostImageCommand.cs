using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common.Classes;

namespace Manager.Application.Medias.PostNewImage.Commands
{
    public sealed record PostImageCommand(IFormCollection Form) : IRequest<Result>;
}