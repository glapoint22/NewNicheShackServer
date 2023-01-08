using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.DisableEnableProduct.Commands
{
    public sealed record DisableEnableProductCommand(Guid ProductId, bool IsNew, Guid NotificationGroupId) : IRequest<Result>;
}