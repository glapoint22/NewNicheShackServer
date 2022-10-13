using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Account.SignUp.Commands;
using Website.Application.Common.Interfaces;

namespace Website.Application.Account.SignUp.Validators
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}