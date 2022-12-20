using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePage.Commands
{
    public sealed record DeletePageCommand(Guid PageId) : IRequest<Result>;
}