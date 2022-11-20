using MediatR;
using Shared.Common.Classes;

namespace Website.Application.ProductOrders.PostOrder.Commands
{
    public sealed record PostOrderCommand(string Notification, string Iv) : IRequest<Result>;
}