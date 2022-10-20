using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.MoveProduct.Commands;

namespace Website.Application.Lists.MoveProduct.Validators
{
    public class MoveProductValidator : AbstractValidator<MoveProductCommand>
    {
        public MoveProductValidator(IWebsiteDbContext dbContext, IUserService userService)
        {
            string userId = userService.GetUserIdFromClaims();
            bool productExists = false;
            bool listExists = false;
            bool canRemove = false;

            RuleFor(x => x.CollaboratorProductId)
                .NotEmpty()

                // Check to see if the product exists
                .MustAsync(async (collaboratorProductId, cancellation) =>
                {
                    productExists = await dbContext.CollaboratorProducts
                        .AnyAsync(x => x.Id == collaboratorProductId);
                    return productExists;
                }).WithMessage("Product does not exist.")


                // Make sure the user has permissions to remove products
                .MustAsync(async (collaboratorProductId, cancellation) =>
                {
                    string? listId = await dbContext.CollaboratorProducts
                        .Where(x => x.Id == collaboratorProductId)
                        .Select(x => x.Collaborator.ListId)
                        .SingleOrDefaultAsync();

                    canRemove = await dbContext.Collaborators.AnyAsync(x => x.ListId == listId && x.UserId == userId &&
                        (x.IsOwner || x.CanRemoveItem), cancellationToken: cancellation);

                    return canRemove;
                })
                .WithMessage("You don't have permissions to remove products from this list.")
                .When(x => productExists, ApplyConditionTo.CurrentValidator);


            RuleFor(x => x.DestinationListId)
                .NotEmpty()

                // Make sure the list exists
                .MustAsync(async (listId, cancellation) =>
                {
                    listExists = await dbContext.Lists.AnyAsync(x => x.Id == listId);
                    return listExists;
                }).WithMessage("List does not exist")
                .When(x => canRemove, ApplyConditionTo.CurrentValidator)



                .MustAsync(async (listId, cancellation) =>
                {
                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId && x.UserId == userId &&
                        (x.IsOwner || x.CanAddToList), cancellationToken: cancellation);
                }).WithMessage("You don't have permissions to add to this list!")
                .When(x => productExists && listExists && canRemove, ApplyConditionTo.CurrentValidator);
        }
    }
}
