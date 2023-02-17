using MediatR;

namespace Manager.Domain.Events
{
    public sealed record HierarchyItemCreatedEvent(string Name) : INotification;
}