using MediatR;

namespace Manager.Domain.Events
{
    public sealed record UserSentEmailEvent(string RecipientEmailMessage, string RecipientEmailAddress, string RecipientName, string EmployeeEmailAddress, string EmployeeEmailMessage) : INotification;
}