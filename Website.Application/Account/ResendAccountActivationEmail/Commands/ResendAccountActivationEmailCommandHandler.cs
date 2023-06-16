using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ResendAccountActivationEmail.Commands
{
    public sealed class ResendAccountActivationEmailCommandHandler : IRequestHandler<ResendAccountActivationEmailCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public ResendAccountActivationEmailCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }


        public async Task<Result> Handle(ResendAccountActivationEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByEmailAsync(request.Email);

            user.AddDomainEvent(new ResendAccountActivationEmailEvent(user.Id));
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}