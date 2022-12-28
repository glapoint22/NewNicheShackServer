using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.PageBuilder.Classes;

namespace Manager.Application.Emails.GetEmail.Queries
{
    public sealed class GetEmailQueryHandler : IRequestHandler<GetEmailQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IPageService _pageService;

        public GetEmailQueryHandler(IManagerDbContext dbContext, IPageService pageService)
        {
            _dbContext = dbContext;
            _pageService = pageService;
        }

        public async Task<Result> Handle(GetEmailQuery request, CancellationToken cancellationToken)
        {
            Email email = await _dbContext.Emails
                .Where(x => x.Id == request.EmailId)
                .SingleAsync();

            PageContent webPage = await _pageService.GetPage(email.Content);

            return Result.Succeeded(new
            {
                Id = request.EmailId,
                email.Name,
                Content = webPage
            });
        }
    }
}