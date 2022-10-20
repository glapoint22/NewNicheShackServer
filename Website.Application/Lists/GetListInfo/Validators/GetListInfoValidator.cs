using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.GetListInfo.Queries;

namespace Website.Application.Lists.GetListInfo.Validators
{
    public class GetListInfoValidator : AbstractValidator<GetListInfoQuery>
    {
        public GetListInfoValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.CollaborateId)
               .NotEmpty()

               // Check to see if the product exists
               .MustAsync(async (collaborateId, cancellation) =>
               {
                   return await dbContext.Lists
                    .AnyAsync(x => x.CollaborateId == collaborateId);
               }).WithMessage("List does not exist.");
        }
    }
}