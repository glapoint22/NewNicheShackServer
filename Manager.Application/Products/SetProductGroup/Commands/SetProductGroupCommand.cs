using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductGroup.Commands
{
    public sealed record SetProductGroupCommand(string ProductId, Guid ProductGroupId, bool Checked) : IRequest<Result>;
}