using Microsoft.AspNetCore.Identity;

namespace Manager.Domain.Entities
{
    public sealed class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Image { get; set; }
    }
}