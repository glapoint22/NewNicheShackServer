﻿using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.CreateDeleteAccountOTP.Commands
{
    public class CreateDeleteAccountOTPCommandHandler : IRequestHandler<CreateDeleteAccountOTPCommand, Result>
    {
        private readonly IUserService _userService;

        public CreateDeleteAccountOTPCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(CreateDeleteAccountOTPCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            string token = await _userService.GenerateDeleteAccountTokenAsync(user);

            // TODO: Send email

            return Result.Succeeded();
        }
    }
}