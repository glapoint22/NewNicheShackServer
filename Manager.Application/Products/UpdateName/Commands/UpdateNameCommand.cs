using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdateName.Commands
{
    public sealed record UpdateNameCommand(string Id, string Name) : IRequest<Result>;
}