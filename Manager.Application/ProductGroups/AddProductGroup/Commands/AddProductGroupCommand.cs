using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.AddProductGroup.Commands
{
    public sealed record AddProductGroupCommand(string Name) : IRequest<Result>;
}