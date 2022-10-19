using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.AddProduct.Commands
{
    public record AddProductCommand(int ProductId, string ListId) : IRequest<Result>;
}