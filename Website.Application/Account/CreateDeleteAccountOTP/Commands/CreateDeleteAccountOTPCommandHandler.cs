using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.CreateDeleteAccountOTP.Commands
{
    public class CreateDeleteAccountOTPCommandHandler : IRequestHandler<CreateDeleteAccountOTPCommand, Result>
    {
        public Task<Result> Handle(CreateDeleteAccountOTPCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}