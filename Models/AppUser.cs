using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public string? Picture { get; set; }
    }
}
