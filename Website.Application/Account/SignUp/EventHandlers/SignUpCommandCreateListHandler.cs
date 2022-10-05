using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Events;

namespace Website.Application.Account.SignUp.EventHandlers
{
    public class SignUpCommandCreateListHandler : INotificationHandler<SignUpSucceededEvent>
    {
        public Task Handle(SignUpSucceededEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
