using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Niches.GetSubniches.Queries;

namespace Website.Application.Niches.GetSubniches.Validators
{
    public sealed class GetSubnichesValidator : AbstractValidator<GetSubnichesQuery>
    {
        public GetSubnichesValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.NicheId)
                .NotEmpty()

                // Check to see if the niche exists
                .MustAsync(async (nicheId, cancellation) =>
                {
                    return await dbContext.Niches
                        .AnyAsync(x => x.Id == nicheId, cancellationToken: cancellation);
                })
                .WithErrorCode("404")
                .WithMessage("Niche not found");
        }
    }
}