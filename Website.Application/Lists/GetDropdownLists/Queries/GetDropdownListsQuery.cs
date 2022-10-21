using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.GetDropdownLists.Queries
{
    public sealed record GetDropdownListsQuery() : IRequest<Result>;
}