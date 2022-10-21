using FluentValidation;
using Website.Application.Account.DeleteAccount.Commands;

namespace Website.Application.Account.DeleteAccount.Validators
{
    public sealed class DeleteAccountValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteAccountValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}