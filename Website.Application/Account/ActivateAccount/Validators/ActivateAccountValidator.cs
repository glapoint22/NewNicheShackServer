using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Account.ActivateAccount.Commands;
using Website.Application.Common.Interfaces;

namespace Website.Application.Account.ActivateAccount.Validators
{
    public class ActivateAccountValidator : AbstractValidator<ActivateAccountCommand>
    {
        public ActivateAccountValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, cancellation) =>
                {
                    return await dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken: cancellation);
                }).WithMessage("The email you provided does not exist");

            RuleFor(x => x.Token)
                .NotEmpty()
                .Length(6);
        }
    }
}
