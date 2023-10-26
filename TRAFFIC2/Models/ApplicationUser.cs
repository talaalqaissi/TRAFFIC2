using Microsoft.AspNetCore.Identity;

namespace TRAFFIC2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public decimal UserId { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
