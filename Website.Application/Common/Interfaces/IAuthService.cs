
using System.Security.Claims;
using Website.Domain.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IAuthService
    {
        string GenerateAccessToken(List<Claim> claims);

        string GenerateRefreshToken(string userId);

        string? GetAccessTokenFromHeader();

        List<Claim> GetClaims(User user, bool isPersistent);

        List<Claim> GetClaims(User user, string provider, bool hasPassword);

        string GetOrderNotificationKey();

        ClaimsPrincipal? GetPrincipalFromToken(string accessToken);
    }
}
