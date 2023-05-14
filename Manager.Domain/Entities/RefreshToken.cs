using System.Security.Cryptography;

namespace Manager.Domain.Entities
{
    public sealed class RefreshToken
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }

        public User User { get; set; } = null!;

        public static RefreshToken Create(string userId, string expiration, string deviceId)
        {
            var randomNumber = new byte[32];
            var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            RefreshToken refreshToken = new()
            {
                Id = Convert.ToBase64String(randomNumber),
                UserId = userId,
                Expiration = DateTime.UtcNow.AddDays(Convert.ToInt32(expiration)),
                DeviceId = deviceId
            };

            return refreshToken;
        }
    }
}