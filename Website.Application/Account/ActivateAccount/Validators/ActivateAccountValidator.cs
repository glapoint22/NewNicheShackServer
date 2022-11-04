using FluentValidation;
using Website.Application.Account.ActivateAccount.Commands;

namespace Website.Application.Account.ActivateAccount.Validators
{
    public sealed class ActivateAccountValidator : AbstractValidator<ActivateAccountCommand>
    {
        public ActivateAccountValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.OneTimePassword)
                .NotEmpty()
                .Length(6);
        }
    }
}
