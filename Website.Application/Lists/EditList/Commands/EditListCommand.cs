using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Lists.EditList.Commands
{
    public sealed record EditListCommand(string Id, string Name, string? Description) : IRequest<Result>;
}