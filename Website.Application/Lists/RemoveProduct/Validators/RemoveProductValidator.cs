using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.RemoveProduct.Commands;

namespace Website.Application.Lists.RemoveProduct.Validators
{
    public class RemoveProductValidator : AbstractValidator<RemoveProductCommand>
    {
        public RemoveProductValidator(IWebsiteDbContext dbContext, IUserService userService)
        {
            string userId = userService.GetUserIdFromClaims();
            bool productExists = false;

            RuleFor(x => x.CollaboratorProductId)
                .NotEmpty()

                // Check to see if the product exists
                .MustAsync(async (collaboratorProductId, cancellation) =>
                {
                    productExists = await dbContext.CollaboratorProducts.AnyAsync(x => x.Id == collaboratorProductId);
                    return productExists;
                }).WithMessage("Product does not exist.")


                // Make sure the user has permissions to remove products
                .MustAsync(async (collaboratorProductId, cancellation) =>
                {
                    string? listId = await dbContext.CollaboratorProducts
                        .Where(x => x.Id == collaboratorProductId)
                        .Select(x => x.Collaborator.ListId)
                        .SingleOrDefaultAsync();

                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId && x.UserId == userId &&
                        (x.IsOwner || x.CanRemoveItem), cancellationToken: cancellation);
                })
                .WithMessage("You don't have permissions to remove products from this list.")
                .When(x => productExists, ApplyConditionTo.CurrentValidator);
        }
    }
}