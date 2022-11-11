using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Pages.GetBrowsePage.Queries;

namespace Website.Application.Pages.GetBrowsePage.Validators
{
    public sealed class GetBrowsePageValidator : AbstractValidator<GetBrowsePageQuery>
    {
        public GetBrowsePageValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x)
                .MustAsync(async (query, cancellation) =>
                {
                    if (query.NicheId == null && query.SubnicheId == null) return false;
                    

                    return await dbContext.Subniches
                        .AnyAsync(x => x.Id == query.SubnicheId || x.NicheId == query.NicheId, cancellationToken: cancellation);
                })
                .WithErrorCode("404");
        }
    }
}