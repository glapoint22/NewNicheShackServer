using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.AddProduct.Commands;

namespace Website.Application.Lists.AddProduct.Validators
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator(IWebsiteDbContext dbContext, IUserService userService)
        {
            string userId = userService.GetUserIdFromClaims();

            RuleFor(x => x.ListId)
                .NotEmpty()
                .MustAsync(async (listId, cancellation) =>
                {
                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId && x.UserId == userId &&
                        (x.IsOwner || x.CanAddToList), cancellationToken: cancellation);
                }).WithMessage("You don't have permissions to add to this list!");

            RuleFor(x => x)
                .NotEmpty()
                .MustAsync(async (command, cancellation) =>
                {
                    var collaboratorIds = await dbContext.Collaborators
                        .Where(x => x.ListId == command.ListId)
                        .Select(x => x.Id)
                        .ToListAsync();

                    return !await dbContext.CollaboratorProducts
                        .AnyAsync(x => x.ProductId == command.ProductId && collaboratorIds
                            .Contains(x.CollaboratorId), cancellationToken: cancellation);
                })
                .WithMessage("List already contains this product");

        }
    }
}