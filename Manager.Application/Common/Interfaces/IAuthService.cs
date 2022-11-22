using System.Security.Claims;

namespace Manager.Application.Common.Interfaces
{
    public interface IAuthService
    {
        string GenerateAccessToken(List<Claim> claims);

        string? GetAccessTokenFromHeader();

        List<Claim> GenerateClaims(string userId, string role, bool isPersistent);


        ClaimsPrincipal? GetPrincipalFromToken(string accessToken);

        DateTimeOffset? GetExpirationFromClaims();

        DateTimeOffset? GetExpirationFromClaims(List<Claim> claims);

        string GetUserIdFromClaims();
    }
}
