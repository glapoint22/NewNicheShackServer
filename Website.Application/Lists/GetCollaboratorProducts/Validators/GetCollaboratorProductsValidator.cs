using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.GetCollaboratorProducts.Queries;

namespace Website.Application.Lists.GetCollaboratorProducts.Validators
{
    public sealed class GetCollaboratorProductsValidator : AbstractValidator<GetCollaboratorProductsQuery>
    {
        public GetCollaboratorProductsValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.ListId)
                .NotEmpty()
                .MustAsync(async (listId, cancellation) =>
                {
                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId, cancellationToken: cancellation);
                }).WithMessage("List does not exist");
        }
    }
}