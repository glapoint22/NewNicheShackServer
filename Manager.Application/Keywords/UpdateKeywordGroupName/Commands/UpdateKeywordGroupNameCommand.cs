using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.UpdateKeywordGroupName.Commands
{
    public sealed record UpdateKeywordGroupNameCommand(Guid Id, string Name) : IRequest<Result>;
}