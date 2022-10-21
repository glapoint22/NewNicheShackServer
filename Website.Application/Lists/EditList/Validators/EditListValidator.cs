using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.EditList.Commands;

namespace Website.Application.Lists.EditList.Validators
{
    public class EditListValidator : AbstractValidator<EditListCommand>
    {
        public EditListValidator(IWebsiteDbContext dbContext, IUserService userService)
        {
            string userId = userService.GetUserIdFromClaims();
            bool listExists = false;

            RuleFor(x => x.Id)
                .NotEmpty()

                // Check to see if the list exists
                .MustAsync(async (listId, cancellation) =>
                {
                    listExists = await dbContext.Lists
                        .AnyAsync(x => x.Id == listId);

                    return listExists;
                }).WithMessage("List does not exists")


                // Check for permissions
                .MustAsync(async (listId, cancellation) =>
                {
                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId && x.UserId == userId &&
                        (x.IsOwner || x.CanEditList), cancellationToken: cancellation);
                }).WithMessage("You don't have permissions to edit this list!")
                .When(x => listExists, ApplyConditionTo.CurrentValidator);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);
        }
    }
}