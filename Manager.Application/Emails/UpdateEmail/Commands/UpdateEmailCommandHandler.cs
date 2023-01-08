using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Emails.UpdateEmail.Commands
{
    public sealed class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public UpdateEmailCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            Email email = await _dbContext.Emails
                .Where(x => x.Id == request.Id)
                .SingleAsync();

            email.Update(request.Type, request.Name, request.Content);

            string userId = _authService.GetUserIdFromClaims();
            email.AddDomainEvent(new EmailModifiedEvent(email.Id, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}