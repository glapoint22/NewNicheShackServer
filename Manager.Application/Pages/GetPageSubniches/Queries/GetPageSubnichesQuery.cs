using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.GetPageSubniches.Queries
{
    public sealed record GetPageSubnichesQuery(Guid PageId) : IRequest<Result>;
}