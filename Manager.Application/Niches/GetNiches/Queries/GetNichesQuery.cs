using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.GetNiches.Queries
{
    public sealed record GetNichesQuery() : IRequest<Result>;
}