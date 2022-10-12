using FluentValidation;
using Website.Application.Account.ResetPassword.Commands;

namespace Website.Application.Account.ResetPassword.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.Token)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}
