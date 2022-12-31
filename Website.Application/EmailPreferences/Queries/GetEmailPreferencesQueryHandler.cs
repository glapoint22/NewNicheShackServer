using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.EmailPreferences.Common;

namespace Website.Application.EmailPreferences.Queries
{
    public sealed class GetEmailPreferencesQueryHandler : IRequestHandler<GetEmailPreferencesQuery, Preferences>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;

        public GetEmailPreferencesQueryHandler(IWebsiteDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Preferences> Handle(GetEmailPreferencesQuery request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            var preferences = await _dbContext.Users
                .Where(x => x.Id == userId)
                .Select(x => new Preferences
                {
                    NameUpdated = x.EmailOnNameUpdated,
                    EmailUpdated = x.EmailOnEmailUpdated,
                    PasswordUpdated = x.EmailOnPasswordUpdated,
                    ProfileImageUpdated = x.EmailOnProfileImageUpdated,
                    CollaboratorJoinedList = x.EmailOnCollaboratorJoinedList,
                    UserJoinedList = x.EmailOnUserJoinedList,
                    UserRemovedFromList = x.EmailOnUserRemovedFromList,
                    CollaboratorRemovedFromList = x.EmailOnCollaboratorRemovedFromList,
                    UserRemovedCollaborator = x.EmailOnUserRemovedCollaborator,
                    CollaboratorAddedListItem = x.EmailOnCollaboratorAddedListItem,
                    UserAddedListItem = x.EmailOnUserAddedListItem,
                    CollaboratorRemovedListItem = x.EmailOnCollaboratorRemovedListItem,
                    UserRemovedListItem = x.EmailOnUserRemovedListItem,
                    CollaboratorMovedListItem = x.EmailOnCollaboratorMovedListItem,
                    UserMovedListItem = x.EmailOnUserMovedListItem,
                    CollaboratorUpdatedList = x.EmailOnCollaboratorUpdatedList,
                    UserUpdatedList = x.EmailOnUserUpdatedList,
                    CollaboratorDeletedList = x.EmailOnCollaboratorDeletedList,
                    UserDeletedList = x.EmailOnUserDeletedList,
                    ItemReviewed = x.EmailOnItemReviewed
                })
                .SingleAsync();

            return preferences;
        }
    }
}