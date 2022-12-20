using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.UpdatePage.Commands
{
    public sealed record UpdatePageCommand(Guid Id, string Name, int PageType, string content) : IRequest<Result>;
}