using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.DeleteList.Commands
{
    public sealed record DeleteListCommand(string Id) : IRequest<Result>;
}