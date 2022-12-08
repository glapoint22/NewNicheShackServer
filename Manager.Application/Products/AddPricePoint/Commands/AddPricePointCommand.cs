using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.AddPricePoint.Commands
{
    public sealed record AddPricePointCommand(string ProductId) : IRequest<Result>;
}