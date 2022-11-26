using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.UpdatePage.Commands
{
    public sealed record UpdatePageCommand(string Id, string Name, int PageType, string content) : IRequest<Result>;
}