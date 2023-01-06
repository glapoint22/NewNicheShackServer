﻿using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.RemoveReview.Commands
{
    public sealed record RemoveReviewCommand(string UserId, Guid ReviewId, bool AddStrike) : IRequest<Result>;
}