using FluentValidation;
using Website.Application.Account.LogIn.Commands;

namespace Website.Application.Account.LogIn.Validators
{
    public sealed class LogInCommandValidator : AbstractValidator<LogInCommand>
    {
        public LogInCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}