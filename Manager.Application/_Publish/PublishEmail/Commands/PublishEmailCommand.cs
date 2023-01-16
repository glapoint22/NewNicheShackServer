using MediatR;
using Shared.Common.Classes;

namespace Manager.Application._Publish.PublishEmail.Commands
{
    public sealed record PublishEmailCommand(Guid EmailId) : IRequest<Result>;
}