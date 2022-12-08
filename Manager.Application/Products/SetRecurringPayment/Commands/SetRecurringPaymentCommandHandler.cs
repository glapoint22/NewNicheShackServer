using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetRecurringPayment.Commands
{
    public sealed class SetRecurringPaymentCommandHandler : IRequestHandler<SetRecurringPaymentCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetRecurringPaymentCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetRecurringPaymentCommand request, CancellationToken cancellationToken)
        {
            Product product = (await _dbContext.Products.FindAsync(request.Id))!;
            product.SetRecurringPayment(request.RecurringPayment);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}