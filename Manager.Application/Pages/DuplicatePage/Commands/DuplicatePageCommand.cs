using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DuplicatePage.Commands
{
    public sealed record DuplicatePageCommand(string Id) : IRequest<Result>;
}