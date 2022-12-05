using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.NewPage.Commands
{
    public sealed record NewPageCommand(string Name, string Content, int PageType) : IRequest<Result>;
}