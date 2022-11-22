using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.UpdateCollaborators.Commands;

namespace Website.Application.Lists.UpdateCollaborators.Validators
{
    public sealed class UpdateCollaboratorsValidator : AbstractValidator<UpdateCollaboratorsCommand>
    {
        public UpdateCollaboratorsValidator(IWebsiteDbContext dbContext, IAuthService authService)
        {
            string userId = authService.GetUserIdFromClaims();
            bool listExists = false;

            RuleFor(x => x.ListId)
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
                        (x.IsOwner || x.CanManageCollaborators), cancellationToken: cancellation);
                }).WithMessage("You don't have permissions to manage collaborators!")
                .When(x => listExists, ApplyConditionTo.CurrentValidator);
        }
    }
}