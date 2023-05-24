using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Messages.PostMessage.Commands
{
    public sealed record PostMessageCommand(string Text) : IRequest<Result>;
}