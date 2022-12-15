using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.UpdateAvailableKeywordGroupName.Commands
{
    public sealed record UpdateAvailableKeywordGroupNameCommand(Guid Id, string Name) : IRequest<Result>;
}