using MediatR;
using Website.Application.Account.LogIn.Commands;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.LogIn.CommandHandlers
{
    public class LogInCommandHandler : IRequestHandler<LogInCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public LogInCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            User user = await _identityService.GetUserByEmailAsync(request.Email);

            if (user == null || await _identityService.CheckPasswordAsync(user, request.Password) == false)
            {
                return Result.Failed(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("No Match", "Your password and email do not match. Please try again.")
                });
            }

            if (!user.EmailConfirmed) return Result.Failed();

            await _identityService.LogInAsync(user, request.IsPersistent);

            return Result.Succeeded();
        }
    }
}