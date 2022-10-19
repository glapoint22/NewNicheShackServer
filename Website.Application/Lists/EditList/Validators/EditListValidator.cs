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

            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(async (listId, cancellation) =>
                {
                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId && x.UserId == userId &&
                        (x.IsOwner || x.CanEditList), cancellationToken: cancellation);
                }).WithMessage("You don't have permissions to edit this list!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);
        }
    }
}