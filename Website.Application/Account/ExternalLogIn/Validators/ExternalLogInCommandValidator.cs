using FluentValidation;
using Website.Application.Account.ExternalLogIn.Commands;

namespace Website.Application.Account.ExternalLogIn.Validators
{
    public sealed class ExternalLogInCommandValidator : AbstractValidator<ExternalLogInCommand>
    {
        public ExternalLogInCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.Provider)
                .NotEmpty();
        }
    }
}