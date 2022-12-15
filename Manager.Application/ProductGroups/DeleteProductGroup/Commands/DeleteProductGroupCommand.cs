using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.DeleteProductGroup.Commands
{
    public sealed record DeleteProductGroupCommand(Guid Id) : IRequest<Result>;
}