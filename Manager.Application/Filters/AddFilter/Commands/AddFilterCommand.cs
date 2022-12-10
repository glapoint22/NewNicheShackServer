using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.AddFilter.Commands
{
    public sealed record AddFilterCommand(string Name) : IRequest<Result>;
}