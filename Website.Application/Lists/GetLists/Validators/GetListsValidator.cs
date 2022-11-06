using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.GetLists.Queries;

namespace Website.Application.Lists.GetLists.Validators
{
    public sealed class GetListsValidator : AbstractValidator<GetListsQuery>
    {
        public GetListsValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.ListId)
                .MustAsync(async (listId, cancellation) =>
                {
                    if (listId == null) return true;

                    return await dbContext.Lists.AnyAsync(x => x.Id == listId, cancellationToken: cancellation);
                })
                .WithErrorCode("404");
        }
    }
}