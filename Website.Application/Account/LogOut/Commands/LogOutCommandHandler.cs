using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.LogOut.Commands
{
    public sealed class LogOutCommandHandler : IRequestHandler<LogOutCommand, Result>
    {
        private readonly ICookieService _cookieService;
        private readonly IWebsiteDbContext _dbContext;

        public LogOutCommandHandler(ICookieService cookieService, IWebsiteDbContext dbContext)
        {
            _cookieService = cookieService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            string? refreshCookie = _cookieService.GetCookie("refresh");

            if (refreshCookie != null)
            {
                RefreshToken? refreshToken = await _dbContext.RefreshTokens
                .FindAsync(refreshCookie);

                if (refreshToken != null)
                {
                    _dbContext.RefreshTokens.Remove(refreshToken);
                    await _dbContext.SaveChangesAsync();
                }
            }

            _cookieService.DeleteCookie("access");
            _cookieService.DeleteCookie("refresh");
            _cookieService.DeleteCookie("user");

            return Result.Succeeded();
        }
    }
}