using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Emails.NewEmail.Commands
{
    public sealed class NewEmailCommandHandler : IRequestHandler<NewEmailCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public NewEmailCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(NewEmailCommand request, CancellationToken cancellationToken)
        {
            Email email = Email.Create(request.Name, request.Content);

            _dbContext.Emails.Add(email);

            string userId = _authService.GetUserIdFromClaims();
            email.AddDomainEvent(new EmailCreatedEvent(email.Id, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(email.Id);
        }
    }
}