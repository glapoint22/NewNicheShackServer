using FluentValidation;
using Website.Application.Account.ChangePassword.Commands;

namespace Website.Application.Account.ChangePassword.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}