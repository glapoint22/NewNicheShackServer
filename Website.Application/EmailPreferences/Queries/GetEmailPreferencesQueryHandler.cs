using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.EmailPreferences.Common;

namespace Website.Application.EmailPreferences.Queries
{
    internal class GetEmailPreferencesQueryHandler : IRequestHandler<GetEmailPreferencesQuery, Preferences>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public GetEmailPreferencesQueryHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Preferences> Handle(GetEmailPreferencesQuery request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

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