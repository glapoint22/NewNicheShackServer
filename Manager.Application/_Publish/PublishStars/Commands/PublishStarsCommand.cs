using MediatR;
using Shared.Common.Classes;

namespace Manager.Application._Publish.PublishStars.Commands
{
    public sealed record PublishStarsCommand() : IRequest<Result>;
}