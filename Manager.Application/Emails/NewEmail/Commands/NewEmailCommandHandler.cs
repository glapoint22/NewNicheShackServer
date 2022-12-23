using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Emails.NewEmail.Commands
{
    public sealed class NewEmailCommandHandler : IRequestHandler<NewEmailCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public NewEmailCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(NewEmailCommand request, CancellationToken cancellationToken)
        {
            Email email = Email.Create(request.Name, request.Content);

            _dbContext.Emails.Add(email);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(email.Id);
        }
    }
}