using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Lists.DeleteList.Commands
{
    public sealed record DeleteListCommand(string Id) : IRequest<Result>;
}