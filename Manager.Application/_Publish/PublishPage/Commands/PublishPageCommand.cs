using MediatR;
using Shared.Common.Classes;

namespace Manager.Application._Publish.PublishPage.Commands
{
    public sealed record PublishPageCommand(Guid PageId) : IRequest<Result>;
}