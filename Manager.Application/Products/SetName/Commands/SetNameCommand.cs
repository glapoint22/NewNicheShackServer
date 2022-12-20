using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetName.Commands
{
    public sealed record SetNameCommand(Guid Id, string Name) : IRequest<Result>;
}