using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePageSubniche.Commands
{
    public sealed record DeletePageSubnicheCommand(Guid PageId, Guid SubnicheId) : IRequest<Result>;
}