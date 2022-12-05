using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.NewPage.Commands
{
    public sealed class NewPageCommandHandler : IRequestHandler<NewPageCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public NewPageCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(NewPageCommand request, CancellationToken cancellationToken)
        {
            Page page = Page.Create(request.Name, request.Content, request.PageType);

            _dbContext.Pages.Add(page);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(new
            {
                page.Id
            });
        }
    }
}