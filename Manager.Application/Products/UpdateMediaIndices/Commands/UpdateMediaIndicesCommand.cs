using Manager.Application.Products.UpdateMediaIndices.Dtos;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdateMediaIndices.Commands
{
    public sealed record UpdateMediaIndicesCommand(string ProductId, List<ProductMediaDto> ProductMedia) : IRequest<Result>;
}