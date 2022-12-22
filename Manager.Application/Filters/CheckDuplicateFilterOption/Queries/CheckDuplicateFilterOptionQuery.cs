using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.CheckDuplicateFilterOption.Queries
{
    public sealed record CheckDuplicateFilterOptionQuery(Guid ChildId, string ChildName) : IRequest<Result>;
}