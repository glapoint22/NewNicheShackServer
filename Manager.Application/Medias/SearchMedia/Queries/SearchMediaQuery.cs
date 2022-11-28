using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Medias.SearchMedia.Queries
{
    public sealed record SearchMediaQuery(int MediaType, string SearchTerm) : IRequest<Result>;
}