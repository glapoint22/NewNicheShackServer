using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Account.SignUp.Commands;
using Website.Application.Common.Interfaces;

namespace Website.Application.Account.SignUp.Validators
{
    internal class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator(IWebsiteDbContext context)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, cancellation) =>
                {
                    bool exists = await context.Users.AnyAsync(x => x.Email == email, cancellationToken: cancellation);
                    return !exists;
                }).WithMessage("The email you entered is already associated with another Niche Shack account. Please provide a different email address.");

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}