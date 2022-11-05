using FluentValidation;
using Website.Application.Account.ResendAccountActivationEmail.Commands;

namespace Website.Application.Account.ResendAccountActivationEmail.Validators
{
    public sealed class ResendAccountActivationEmailValidator : AbstractValidator<ResendAccountActivationEmailCommand>
    {
        public ResendAccountActivationEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}