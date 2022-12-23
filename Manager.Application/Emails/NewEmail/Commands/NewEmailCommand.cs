using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Emails.NewEmail.Commands
{
    public sealed record NewEmailCommand(string Name, string Content) : IRequest<Result>;
}