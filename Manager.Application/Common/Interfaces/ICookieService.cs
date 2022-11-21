namespace Manager.Application.Common.Interfaces
{
    public interface ICookieService
    {
        void DeleteCookie(string cookie);

        string? GetCookie(string cookie);

        void SetCookie(string cookie, string value, DateTimeOffset? expiration);
    }
}