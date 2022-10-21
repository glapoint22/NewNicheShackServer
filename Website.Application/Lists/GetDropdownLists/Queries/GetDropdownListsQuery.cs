using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.GetDropdownLists.Queries
{
    public record GetDropdownListsQuery() : IRequest<Result>;
}