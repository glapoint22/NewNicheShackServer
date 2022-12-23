using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Emails.DuplicateEmail.Commands
{
    public sealed record DuplicateEmailCommand(Guid Id) : IRequest<Result>;
}