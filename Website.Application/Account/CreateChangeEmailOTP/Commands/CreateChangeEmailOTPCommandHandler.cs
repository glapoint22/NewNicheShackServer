using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.CreateChangeEmailOTP.Commands
{
    public sealed class CreateChangeEmailOTPCommandHandler : IRequestHandler<CreateChangeEmailOTPCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public CreateChangeEmailOTPCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(CreateChangeEmailOTPCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            user.AddDomainEvent(new UserChangedEmailOtpEvent(user.Id, request.Email));
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}