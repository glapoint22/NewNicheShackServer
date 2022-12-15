using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetAvailableKeywords.Queries
{
    public sealed record GetAvailableKeywordsQuery(Guid ParenetId) : IRequest<Result>;
}