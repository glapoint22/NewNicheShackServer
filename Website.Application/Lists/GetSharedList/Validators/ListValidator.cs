using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.GetSharedList.Queries;

namespace Website.Application.Lists.GetSharedList.Validators
{
    public class ListValidator : AbstractValidator<GetSharedListQuery>
    {
        public ListValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.ListId)
                .NotEmpty()
                .MustAsync(async (listId, cancellation) =>
                {
                    return await dbContext.Lists.AnyAsync(x => x.Id == listId, cancellationToken: cancellation);
                }).WithMessage("List does not exist");
        }
    }
}