using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.SearchLinkPages.Queries
{
    public sealed record SearchLinkPagesQuery(string SearchTerm) : IRequest<Result>;
}