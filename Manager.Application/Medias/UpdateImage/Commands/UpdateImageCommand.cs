using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common.Classes;

namespace Manager.Application.Medias.UpdateImage.Commands
{
    public sealed record UpdateImageCommand(IFormCollection Form) : IRequest<Result>;
}