using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.SetFilterName.Commands
{
    public sealed record SetFilterNameCommand(Guid Id, string Name) : IRequest<Result>;
}