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

            RuleFor(x => x.ListId)
                .NotEmpty()

                // Check for permissions
                .MustAsync(async (listId, cancellation) =>
                {
                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId &&
                        x.UserId == userId &&
                        (x.IsOwner || x.CanManageCollaborators), cancellationToken: cancellation);
                });
        }
    }
}