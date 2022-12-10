using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.SetFilterOptionName.Commands
{
    public sealed record SetFilterOptionNameCommand(Guid Id, string Name) : IRequest<Result>;
}