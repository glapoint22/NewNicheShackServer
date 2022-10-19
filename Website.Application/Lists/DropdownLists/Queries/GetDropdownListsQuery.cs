using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.DropdownLists.Queries
{
    public record GetDropdownListsQuery() : IRequest<Result>;
}