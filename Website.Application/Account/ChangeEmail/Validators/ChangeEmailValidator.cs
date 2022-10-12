using FluentValidation;
using Website.Application.Account.ChangeEmail.Commands;

namespace Website.Application.Account.ChangeEmail.Validators
{
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailCommand>
    {
        public ChangeEmailValidator()
        {
            RuleFor(x => x.OneTimePassword)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.NewEmail)
                .NotEmpty();
        }
    }
}