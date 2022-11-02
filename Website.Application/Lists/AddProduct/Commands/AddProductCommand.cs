using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.AddProduct.Commands
{
    public sealed record AddProductCommand(string ProductId, int CollaboratorId) : IRequest<Result>;
}