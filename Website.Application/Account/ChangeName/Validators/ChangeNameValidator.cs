using FluentValidation;
using Website.Application.Account.ChangeName.Commands;

namespace Website.Application.Account.ChangeName.Validators
{
    public sealed class ChangeNameValidator : AbstractValidator<ChangeNameCommand>
    {
        public ChangeNameValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
