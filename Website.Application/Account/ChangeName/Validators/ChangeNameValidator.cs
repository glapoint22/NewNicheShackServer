using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Application.Account.AddPassword.Commands;
using Website.Application.Account.ChangeName.Commands;

namespace Website.Application.Account.ChangeName.Validators
{
    public class ChangeNameValidator : AbstractValidator<ChangeNameCommand>
    {
        public ChangeNameValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
