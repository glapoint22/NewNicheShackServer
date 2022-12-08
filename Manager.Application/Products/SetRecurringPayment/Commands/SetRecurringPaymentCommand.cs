using MediatR;
using Shared.Common.Classes;
using Shared.Common.ValueObjects;

namespace Manager.Application.Products.SetRecurringPayment.Commands
{
    public sealed record SetRecurringPaymentCommand(string Id, RecurringPayment RecurringPayment) : IRequest<Result>;
}