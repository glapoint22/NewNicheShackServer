using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Emails.DuplicateEmail.Commands
{
    public sealed class DuplicateEmailCommandHandler : IRequestHandler<DuplicateEmailCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DuplicateEmailCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DuplicateEmailCommand request, CancellationToken cancellationToken)
        {
            Email email = await _dbContext.Emails
                .Where(x => x.Id == request.Id)
                .SingleAsync();

            Email duplicateEmail = email.Duplicate();
            _dbContext.Emails.Add(duplicateEmail);


            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(duplicateEmail.Id);
        }
    }
}