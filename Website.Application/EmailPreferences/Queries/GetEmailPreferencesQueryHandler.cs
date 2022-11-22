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
                    NameChange = x.EmailOnNameChange,
                    EmailChange = x.EmailOnEmailChange,
                    PasswordChange = x.EmailOnPasswordChange,
                    ProfileImageChange = x.EmailOnProfileImageChange,
                    NewCollaborator = x.EmailOnNewCollaborator,
                    RemovedCollaborator = x.EmailOnRemovedCollaborator,
                    RemovedListItem = x.EmailOnRemovedListItem,
                    MovedListItem = x.EmailOnMovedListItem,
                    AddedListItem = x.EmailOnAddedListItem,
                    EditedList = x.EmailOnEditedList,
                    DeletedList = x.EmailOnDeletedList,
                    Review = x.EmailOnReview
                })
                .SingleAsync();

            return preferences;
        }
    }
}