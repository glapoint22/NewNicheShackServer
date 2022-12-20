using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.AddProduct.Commands
{
    public sealed record AddProductCommand(Guid Id, string Name) : IRequest<Result>;
}