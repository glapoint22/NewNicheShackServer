using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.SearchPages.Queries
{
    public sealed record SearchPagesQuery(string SearchTerm) : IRequest<Result>;
}