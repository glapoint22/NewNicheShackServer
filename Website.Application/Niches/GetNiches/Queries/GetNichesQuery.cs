using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Niches.GetNiches.Queries
{
    public sealed record GetNichesQuery() : IRequest<Result>;
}