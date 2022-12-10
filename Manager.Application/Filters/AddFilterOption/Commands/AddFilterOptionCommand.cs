using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.AddFilterOption.Commands
{
    public sealed record AddFilterOptionCommand(Guid Id, string Name) : IRequest<Result>;
}