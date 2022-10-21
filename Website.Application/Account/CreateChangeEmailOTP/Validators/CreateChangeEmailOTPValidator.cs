using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Account.CreateChangeEmailOTP.Commands;
using Website.Application.Common.Interfaces;

namespace Website.Application.Account.CreateChangeEmailOTP.Validators
{
    public sealed class CreateChangeEmailOTPValidator : AbstractValidator<CreateChangeEmailOTPCommand>
    {
        public CreateChangeEmailOTPValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}