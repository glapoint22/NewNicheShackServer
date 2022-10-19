using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.CreateList.Commands
{
    public record CreateListCommand(string Name, string Description) : IRequest<Result>;
}