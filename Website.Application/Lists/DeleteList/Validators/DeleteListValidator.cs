using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.DeleteList.Commands;

namespace Website.Application.Lists.DeleteList.Validators
{
    public class DeleteListValidator : AbstractValidator<DeleteListCommand>
    {
        public DeleteListValidator(IWebsiteDbContext dbContext, IUserService userService)
        {
            string userId = userService.GetUserIdFromClaims();
            bool listExists = false;

            RuleFor(x => x.Id)
                .NotEmpty()

                .MustAsync(async (listId, cancellation) =>
                {
                    listExists = await dbContext.Lists
                        .AnyAsync(x => x.Id == listId);

                    return listExists;
                }).WithMessage("List does not exists")


                .MustAsync(async (listId, cancellation) =>
                {
                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId && x.UserId == userId &&
                        (x.IsOwner || x.CanDeleteList), cancellationToken: cancellation);
                }).WithMessage("You don't have permissions to delete this list!")
                .When(x => listExists, ApplyConditionTo.CurrentValidator);
        }
    }
}