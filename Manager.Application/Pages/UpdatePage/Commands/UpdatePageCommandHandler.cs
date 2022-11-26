using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.UpdatePage.Commands
{
    public sealed class UpdatePageCommandHandler : IRequestHandler<UpdatePageCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdatePageCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
        {
            Page page = (await _dbContext.Pages.FindAsync(request.Id))!;

            page.Update(request.Name, request.content, request.PageType);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}