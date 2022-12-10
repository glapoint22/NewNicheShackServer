using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.DeleteFilter.Commands
{
    public sealed record DeleteFilterCommand(Guid Id) : IRequest<Result>;
}