using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Emails.UpdateEmail.Commands
{
    public sealed class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateEmailCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            Email email = await _dbContext.Emails
                .Where(x => x.Id == request.Id)
                .SingleAsync();

            email.Update(request.Name, request.Content);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}