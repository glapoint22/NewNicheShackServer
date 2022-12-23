using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Emails.DeleteEmail.Commands
{
    public sealed record DeleteEmailCommand(Guid EmailId) : IRequest<Result>;
}