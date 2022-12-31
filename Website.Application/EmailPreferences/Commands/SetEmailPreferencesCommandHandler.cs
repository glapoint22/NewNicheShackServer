using MediatR;
using Shared.Common.Classes;
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


            var emailPreferences = request.Preferences;

            user.EmailOnNameUpdated = emailPreferences.NameUpdated;
            user.EmailOnEmailUpdated = emailPreferences.EmailUpdated;
            user.EmailOnPasswordUpdated = emailPreferences.PasswordUpdated;
            user.EmailOnProfileImageUpdated = emailPreferences.ProfileImageUpdated;
            user.EmailOnCollaboratorJoinedList = emailPreferences.CollaboratorJoinedList;
            user.EmailOnUserJoinedList = emailPreferences.UserJoinedList;
            user.EmailOnUserRemovedFromList = emailPreferences.UserRemovedFromList;
            user.EmailOnCollaboratorRemovedFromList = emailPreferences.CollaboratorRemovedFromList;
            user.EmailOnUserRemovedCollaborator = emailPreferences.UserRemovedCollaborator;
            user.EmailOnCollaboratorAddedListItem = emailPreferences.CollaboratorAddedListItem;
            user.EmailOnUserAddedListItem = emailPreferences.UserAddedListItem;
            user.EmailOnCollaboratorRemovedListItem = emailPreferences.CollaboratorRemovedListItem;
            user.EmailOnUserRemovedListItem = emailPreferences.UserRemovedListItem;
            user.EmailOnCollaboratorMovedListItem = emailPreferences.CollaboratorMovedListItem;
            user.EmailOnUserMovedListItem = emailPreferences.UserMovedListItem;
            user.EmailOnCollaboratorUpdatedList = emailPreferences.CollaboratorUpdatedList;
            user.EmailOnUserUpdatedList = emailPreferences.UserUpdatedList;
            user.EmailOnCollaboratorDeletedList = emailPreferences.CollaboratorDeletedList;
            user.EmailOnUserDeletedList = emailPreferences.UserDeletedList;
            user.EmailOnItemReviewed = emailPreferences.ItemReviewed;

            await _userService.UpdateAsync(user);

            return Result.Succeeded();
        }
    }
}
