using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Application.Common.Classes;

namespace Website.Application.Account.DeleteRefreshToken.Commands
{
    public record DeleteRefreshTokenCommand(string NewRefreshToken) : IRequest<Result>;
}
