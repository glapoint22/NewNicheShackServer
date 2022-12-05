using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.AddPageSubniche.Commands
{
    public sealed record AddPageSubnicheCommand(string PageId, string SubnicheId) : IRequest<Result>;
}