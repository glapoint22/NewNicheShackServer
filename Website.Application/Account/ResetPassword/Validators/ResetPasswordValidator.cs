using FluentValidation;
using Website.Application.Account.ResetPassword.Commands;

namespace Website.Application.Account.ResetPassword.Validators
{
    public sealed class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.OneTimePassword)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}