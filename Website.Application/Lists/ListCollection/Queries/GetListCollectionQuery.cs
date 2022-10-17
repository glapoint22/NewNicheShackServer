using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.ListCollection.Queries
{
    public record GetListCollectionQuery() : IRequest<Result>;
}