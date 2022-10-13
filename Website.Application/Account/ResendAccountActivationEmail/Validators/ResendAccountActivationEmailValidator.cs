using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Account.ResendAccountActivationEmail.Commands;
using Website.Application.Common.Interfaces;

namespace Website.Application.Account.ResendAccountActivationEmail.Validators
{
    public class ResendAccountActivationEmailValidator : AbstractValidator<ResendAccountActivationEmailCommand>
    {
        public ResendAccountActivationEmailValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, cancellation) =>
                {
                    return await dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken: cancellation);
                }).WithMessage("The email you provided does not exist");
        }
    }
}