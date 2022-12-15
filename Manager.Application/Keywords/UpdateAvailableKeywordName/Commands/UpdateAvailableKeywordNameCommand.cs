using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.UpdateAvailableKeywordName.Commands
{
    public sealed record UpdateAvailableKeywordNameCommand(Guid Id, string Name) : IRequest<Result>;
}