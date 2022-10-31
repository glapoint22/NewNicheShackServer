using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Pages.GetPage.Queries
{
    public sealed record GetPageQuery(string? Id, int? PageType) : IRequest<Result>;
}