using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.NewPage.Commands
{
    public sealed class NewPageCommandHandler : IRequestHandler<NewPageCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public NewPageCommandHandler(IManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(NewPageCommand request, CancellationToken cancellationToken)
        {
            Page page = Page.Create(request.Name, request.Content, request.PageType);

            _dbContext.Pages.Add(page);

            string userId = _authService.GetUserIdFromClaims();
            page.AddDomainEvent(new PageCreatedEvent(page.Id, userId));

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(page.Id);
        }
    }
}