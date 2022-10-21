using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.EditList.Commands
{
    public sealed record EditListCommand(string Id, string Name, string Description) : IRequest<Result>;
}