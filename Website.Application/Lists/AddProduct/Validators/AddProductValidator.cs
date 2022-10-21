using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.AddProduct.Commands;

namespace Website.Application.Lists.AddProduct.Validators
{
    public sealed class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator(IWebsiteDbContext dbContext, IUserService userService)
        {
            string userId = userService.GetUserIdFromClaims();
            bool productExists = false;
            bool productIsUnique = false;

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .MustAsync(async (productId, cancellation) =>
                {
                    productExists = await dbContext.Products.AnyAsync(x => x.Id == productId);
                    return productExists;
                })
                .WithMessage("Product does not exist");

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) =>
                {
                    var collaboratorIds = await dbContext.Collaborators
                        .Where(x => x.Id == command.CollaboratorId)
                        .SelectMany(x => x.List.Collaborators)
                        .Select(x => x.Id)
                        .ToListAsync();

                    productIsUnique =!await dbContext.CollaboratorProducts
                        .AnyAsync(x => x.ProductId == command.ProductId && collaboratorIds
                        .Contains(x.CollaboratorId), cancellationToken: cancellation);

                    return productIsUnique;
                })
                .WithMessage("List already contains that product")
                .When(x => productExists, ApplyConditionTo.CurrentValidator);


            RuleFor(x => x.CollaboratorId)
                .NotEmpty()
                .MustAsync(async (collaboratorId, cancellation) =>
                {
                    return await dbContext.Collaborators.AnyAsync(x => x.Id == collaboratorId && x.UserId == userId &&
                        (x.IsOwner || x.CanAddToList), cancellationToken: cancellation);
                }).WithMessage("You don't have permissions to add to this list!")
                .When(x => productExists && productIsUnique, ApplyConditionTo.CurrentValidator);
        }
    }
}