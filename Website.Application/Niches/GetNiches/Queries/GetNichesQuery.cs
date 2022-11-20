using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Niches.GetNiches.Queries
{
    public sealed record GetNichesQuery() : IRequest<Result>;
}