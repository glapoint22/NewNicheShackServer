using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.GetCollaborators.Queries;

namespace Website.Application.Lists.GetCollaborators.Validators
{
    public sealed class GetCollaboratorsValidator : AbstractValidator<GetCollaboratorsQuery>
    {
        public GetCollaboratorsValidator(IWebsiteDbContext dbContext, IUserService userService)
        {
            string userId = userService.GetUserIdFromClaims();
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
                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId && 
                        x.UserId == userId && 
                        (x.IsOwner || x.CanManageCollaborators), cancellationToken: cancellation);
                }).WithMessage("You do not have permissions to access this content!")
                .When(x => listExists, ApplyConditionTo.CurrentValidator);
        }
    }
}