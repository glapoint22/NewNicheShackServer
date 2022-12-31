using MediatR;
using Shared.Common.Classes;
using Shared.EmailBuilder.Classes;

namespace Manager.Application.Emails.UpdateEmail.Commands
{
    public sealed record UpdateEmailCommand(Guid Id, EmailType Type, string Name, string Content) : IRequest<Result>;
}