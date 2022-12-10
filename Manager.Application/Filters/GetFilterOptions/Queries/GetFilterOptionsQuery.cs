using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.GetFilterOptions.Queries
{
    public sealed record GetFilterOptionsQuery(Guid ParentId, string ProductId) : IRequest<Result>;
}