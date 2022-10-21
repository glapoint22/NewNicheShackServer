using FluentValidation;
using Website.Application.Lists.CreateList.Commands;

namespace Website.Application.Lists.CreateList.Validators
{
    public sealed class CreateListCommandValidator : AbstractValidator<CreateListCommand>
    {
        public CreateListCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);
        }
    }
}