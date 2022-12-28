using MediatR;

namespace Website.Domain.Events
{
    public sealed record UserForgotPasswordEvent(string UserId) : INotification;
}