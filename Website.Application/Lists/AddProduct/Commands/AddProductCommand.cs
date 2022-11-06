using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.AddProduct.Commands
{
    public sealed record AddProductCommand(string ProductId, string ListId) : IRequest<Result>;
}