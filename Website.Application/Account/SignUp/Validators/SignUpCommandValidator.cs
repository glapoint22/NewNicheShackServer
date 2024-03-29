﻿using FluentValidation;
using Website.Application.Account.SignUp.Commands;

namespace Website.Application.Account.SignUp.Validators
{
    public sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}