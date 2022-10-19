using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.EditList.Commands
{
    public record EditListCommand(string Id, string Name, string Description) : IRequest<Result>;
}