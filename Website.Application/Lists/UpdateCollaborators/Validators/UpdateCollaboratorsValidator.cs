using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.UpdateCollaborators.Commands;

namespace Website.Application.Lists.UpdateCollaborators.Validators
{
    public class UpdateCollaboratorsValidator : AbstractValidator<UpdateCollaboratorsCommand>
    {
        public UpdateCollaboratorsValidator(IWebsiteDbContext dbContext, IUserService userService)
        {
            string userId = userService.GetUserIdFromClaims();

            RuleFor(x => x.ListId)
                .NotEmpty()
                .MustAsync(async (listId, cancellation) =>
                {
                    return await dbContext.Collaborators.AnyAsync(x => x.ListId == listId && x.UserId == userId &&
                        (x.IsOwner || x.CanManageCollaborators), cancellationToken: cancellation);
                }).WithMessage("You don't have permissions to manage collaborators!");
        }
    }
}
