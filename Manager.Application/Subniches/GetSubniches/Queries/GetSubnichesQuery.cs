using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.GetSubniches.Queries
{
    public sealed record GetSubnichesQuery(string ParentId) : IRequest<Result>;
}