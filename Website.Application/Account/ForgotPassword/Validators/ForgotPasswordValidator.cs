using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Account.ForgotPassword.Commands;
using Website.Application.Common.Interfaces;

namespace Website.Application.Account.ForgotPassword.Validators
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MustAsync(async (email, cancellation) =>
                {
                    return await dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken: cancellation);
                }).WithMessage("The email you provided does not exist");
        }
    }
}