using Manager.Domain.Dtos;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductMediaIndices.Commands
{
    public sealed record SetProductMediaIndicesCommand(string ProductId, List<ProductMediaDto> ProductMedia) : IRequest<Result>;
}