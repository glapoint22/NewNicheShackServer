using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.GetPage.Queries
{
    public sealed class GetPageQueryHandler : IRequestHandler<GetPageQuery, Result>
    {
        public Task<Result> Handle(GetPageQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}