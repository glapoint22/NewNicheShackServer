using MediatR;
using Microsoft.AspNetCore.Identity;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.ChangePassword.Commands
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;

        public ChangePasswordCommandHandler(IUserService userService, ITaskService taskService)
        {
            _userService = userService;
            _taskService = taskService;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();
            string key = "ChangePasswordCommandHandler" + user.Id;

            if (user != null)
            {
                if (!_taskService.CompletedTasks.Contains(key))
                {
                    IdentityResult result = await _userService.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

                    if (result.Succeeded)
                    {
                        _taskService.CompletedTasks.Add(key);
                    }
                    else
                    {
                        return Result.Failed();
                    }
                }

                // TODO: Send email
                _taskService.CompletedTasks.Remove(key);
                return Result.Succeeded();

            }

            throw new Exception("Error while trying to get user from claims.");
        }
    }
}