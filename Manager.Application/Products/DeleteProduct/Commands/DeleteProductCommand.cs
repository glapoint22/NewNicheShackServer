using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.DeleteProduct.Commands
{
    public sealed record DeleteProductCommand(Guid Id) : IRequest<Result>;
}