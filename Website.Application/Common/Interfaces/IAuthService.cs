using System.Security.Claims;

namespace Website.Application.Common.Interfaces
{
    public interface IAuthService
    {
        string GenerateAccessToken(List<Claim> claims);

        string? GetAccessTokenFromHeader();

        List<Claim> GenerateClaims(string userId, string role, bool isPersistent);

        List<Claim> GenerateClaims(string userId, string role, string provider, bool hasPassword);

        ClaimsPrincipal? GetPrincipalFromToken(string accessToken);
    }
}
