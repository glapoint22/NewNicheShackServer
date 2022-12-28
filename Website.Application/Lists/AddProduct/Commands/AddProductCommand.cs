using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Lists.AddProduct.Commands
{
    public sealed record AddProductCommand(Guid ProductId, string ListId) : IRequest<Result>;
}