using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductFilter.Commands
{
    public sealed record SetProductFilterCommand(string ProductId, Guid FilterOptionId, bool Checked) : IRequest<Result>;
}