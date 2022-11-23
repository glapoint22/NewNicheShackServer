using MediatR;
using Shared.Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.Notifications.AddNoncompliantStrikeUserName.Commands
{
    public sealed record AddNoncompliantStrikeUserNameCommand(string UserId, string UserName) : IRequest<Result>;
}
