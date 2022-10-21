using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.AddCollaborator.Commands;

namespace Website.Application.Lists.AddCollaborator.Validators
{
    public sealed class AddCollaboratorValidator : AbstractValidator<AddCollaboratorCommand>
    {
        public AddCollaboratorValidator(IWebsiteDbContext dbContext, IUserService userService)
        {
            string userId = userService.GetUserIdFromClaims();
            bool listExists = false;

            RuleFor(x => x.CollaborateId)
               .NotEmpty()

               // Check to see if the list exists
               .MustAsync(async (collaborateId, cancellation) =>
               {
                   listExists = await dbContext.Lists
                    .AnyAsync(x => x.CollaborateId == collaborateId);

                   return listExists;
               }).WithMessage("List does not exist.")

               // Check to see if the user is already collaborating on this list
               .MustAsync(async (collaborateId, cancellation) =>
                {
                    string listId = await dbContext.Lists
                        .Where(x => x.CollaborateId == collaborateId)
                        .Select(x => x.Id)
                        .SingleAsync();

                    return !await dbContext.Collaborators
                        .AnyAsync(x => x.UserId == userId && x.ListId == listId);
                }).WithMessage("User is already collaborating on this list.")
                .When(x => listExists, ApplyConditionTo.CurrentValidator);
        }
    }
}