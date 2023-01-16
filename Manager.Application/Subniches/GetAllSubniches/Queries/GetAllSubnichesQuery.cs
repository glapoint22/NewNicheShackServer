using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.GetAllSubniches.Queries
{
    public sealed record GetAllSubnichesQuery() : IRequest<Result>;
}