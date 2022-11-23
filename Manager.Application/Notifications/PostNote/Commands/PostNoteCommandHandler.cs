using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.PostNote.Commands
{
    public sealed class PostNoteCommandHandler : IRequestHandler<PostNoteCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public PostNoteCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(PostNoteCommand request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            NotificationEmployeeNote notificationEmployeeNote = NotificationEmployeeNote
                .Create(request.NotificationGroupId, request.NotificationId, userId, request.Note);

            _dbContext.NotificationEmployeeNotes.Add(notificationEmployeeNote);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}