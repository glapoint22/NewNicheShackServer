using MediatR;
using Microsoft.AspNetCore.Identity;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeEmail.Commands
{
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly ITaskService _taskService;

        public ChangeEmailCommandHandler(IUserService userService, IWebsiteDbContext dbContext, ITaskService taskService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _taskService = taskService;
        }

        public async Task<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user != null)
            {
                string key = "ChangeEmailCommandHandler-" + user.Id;

                if (await _userService.CheckPasswordAsync(user, request.Password))
                {
                    if (!_taskService.CompletedTasks.Contains(key))
                    {
                        IdentityResult result = await _userService.ChangeEmailAsync(user, request.NewEmail, request.OneTimePassword);

                        if (result.Succeeded)
                        {
                            _taskService.CompletedTasks.Add(key);
                        }
                        else
                        {
                            return Result.Failed();
                        }
                    }
                    else
                    {
                        user.AddDomainEvent(new UserChangedEmailEvent(user));
                    }

                    await _dbContext.SaveChangesAsync();

                    _taskService.CompletedTasks.Remove(key);

                    return Result.Succeeded();
                }
            }

            throw new Exception("Error while trying to get user from claims.");
        }
    }
}