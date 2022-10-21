using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.GetLists.Queries
{
    public record GetListsQuery() : IRequest<Result>;
}