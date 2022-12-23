using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Emails.DeleteEmail.Commands
{
    public sealed class DeleteEmailCommandHandler : IRequestHandler<DeleteEmailCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteEmailCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteEmailCommand request, CancellationToken cancellationToken)
        {
            Email email = await _dbContext.Emails
                .Where(x => x.Id == request.EmailId)
                .SingleAsync();

            _dbContext.Emails.Remove(email);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}