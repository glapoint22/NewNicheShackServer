using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.AddPageSubniche.Commands
{
    public sealed record AddPageSubnicheCommand(Guid PageId, Guid SubnicheId) : IRequest<Result>;
}