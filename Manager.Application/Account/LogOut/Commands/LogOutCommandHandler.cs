using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Account.LogOut.Commands
{
    public sealed class LogOutCommandHandler : IRequestHandler<LogOutCommand, Result>
    {
        private readonly ICookieService _cookieService;
        private readonly IManagerDbContext _dbContext;

        public LogOutCommandHandler(ICookieService cookieService, IManagerDbContext dbContext)
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
            _cookieService.DeleteCookie("device");

            return Result.Succeeded();
        }
    }
}