using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Emails.DuplicateEmail.Commands
{
    public sealed class DuplicateEmailCommandHandler : IRequestHandler<DuplicateEmailCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public DuplicateEmailCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(DuplicateEmailCommand request, CancellationToken cancellationToken)
        {
            Email email = await _dbContext.Emails
                .Where(x => x.Id == request.Id)
                .SingleAsync();

            Email duplicateEmail = email.Duplicate();
            _dbContext.Emails.Add(duplicateEmail);

            string userId = _authService.GetUserIdFromClaims();
            email.AddDomainEvent(new EmailCreatedEvent(email.Id, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(duplicateEmail.Id);
        }
    }
}