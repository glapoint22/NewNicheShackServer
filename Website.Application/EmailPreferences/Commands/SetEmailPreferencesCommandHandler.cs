using MediatR;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.EmailPreferences.Commands
{
    public sealed class SetEmailPreferencesCommandHandler : IRequestHandler<SetEmailPreferencesCommand, Result>
    {
        private readonly IUserService _userService;

        public SetEmailPreferencesCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(SetEmailPreferencesCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user != null)
            {
                var emailPreferences = request.Preferences;

                user.EmailOnNameChange = emailPreferences.NameChange;
                user.EmailOnEmailChange = emailPreferences.EmailChange;
                user.EmailOnPasswordChange = emailPreferences.PasswordChange;
                user.EmailOnProfileImageChange = emailPreferences.ProfileImageChange;
                user.EmailOnNewCollaborator = emailPreferences.NewCollaborator;
                user.EmailOnRemovedCollaborator = emailPreferences.RemovedCollaborator;
                user.EmailOnRemovedListItem = emailPreferences.RemovedListItem;
                user.EmailOnMovedListItem = emailPreferences.MovedListItem;
                user.EmailOnAddedListItem = emailPreferences.AddedListItem;
                user.EmailOnEditedList = emailPreferences.EditedList;
                user.EmailOnDeletedList = emailPreferences.DeletedList;
                user.EmailOnReview = emailPreferences.Review;

                await _userService.UpdateAsync(user);

                return Result.Succeeded();
            }

            throw new Exception("Error while trying to get user from claims.");
        }
    }
}
