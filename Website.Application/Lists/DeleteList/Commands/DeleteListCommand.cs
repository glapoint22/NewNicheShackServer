using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.DeleteList.Commands
{
    public record DeleteListCommand(string Id) : IRequest<Result>;
}