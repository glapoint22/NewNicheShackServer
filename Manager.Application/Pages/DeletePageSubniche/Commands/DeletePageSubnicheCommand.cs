using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePageSubniche.Commands
{
    public sealed record DeletePageSubnicheCommand(string PageId, string SubnicheId) : IRequest<Result>;
}