using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Lists.CreateList.Commands
{
    public sealed record CreateListCommand(string Name, string Description) : IRequest<Result>;
}