using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.AddSubproduct.Commands
{
    public sealed record AddSubproductCommand(Guid ProductId, int Type) : IRequest<Result>;
}