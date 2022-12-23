using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Emails.UpdateEmail.Commands
{
    public sealed record UpdateEmailCommand(Guid Id, string Name, string Content) : IRequest<Result>;
}