using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.UpdateKeywordName.Commands
{
    public sealed record UpdateKeywordNameCommand(Guid Id, string Name) : IRequest<Result>;
}