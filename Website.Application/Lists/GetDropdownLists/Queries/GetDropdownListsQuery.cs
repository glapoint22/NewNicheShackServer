using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Lists.GetDropdownLists.Queries
{
    public sealed record GetDropdownListsQuery() : IRequest<Result>;
}