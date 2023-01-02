﻿using MediatR;

namespace Manager.Domain.Events
{
    public sealed record UserReceivedNoncompliantStrikeListEvent(string FirstName, string LastName, string Email, string ListName, string? ListDescription) : INotification;
}