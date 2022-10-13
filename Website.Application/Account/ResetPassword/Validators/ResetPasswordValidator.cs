using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Account.ResetPassword.Commands;
using Website.Application.Common.Interfaces;

namespace Website.Application.Account.ResetPassword.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MustAsync(async (email, cancellation) =>
                {
                    return await dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken: cancellation);
                }).WithMessage("The email you provided does not exist");

            RuleFor(x => x.Token)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}
