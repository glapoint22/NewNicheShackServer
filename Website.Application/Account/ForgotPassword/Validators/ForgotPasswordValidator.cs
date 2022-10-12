﻿using FluentValidation;
using Website.Application.Account.ForgotPassword.Commands;

namespace Website.Application.Account.ForgotPassword.Validators
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();
        }
    }
}