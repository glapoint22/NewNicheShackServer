using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.SetProductGroupName.Commands
{
    public sealed record SetProductGroupNameCommand(Guid Id, string Name) : IRequest<Result>;
}