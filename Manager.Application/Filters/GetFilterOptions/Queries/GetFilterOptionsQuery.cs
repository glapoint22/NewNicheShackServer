using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.GetFilterOptions.Queries
{
    public sealed record GetFilterOptionsQuery(Guid ParentId, Guid ProductId) : IRequest<Result>;
}