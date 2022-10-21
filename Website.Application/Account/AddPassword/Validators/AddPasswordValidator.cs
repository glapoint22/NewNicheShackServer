using FluentValidation;
using Website.Application.Account.AddPassword.Commands;

namespace Website.Application.Account.AddPassword.Validators
{
    public sealed class AddPasswordValidator : AbstractValidator<AddPasswordCommand>
    {
        public AddPasswordValidator()
        {
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}