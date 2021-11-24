using Microsoft.AspNetCore.Identity;

namespace Pentago.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int Elo { get; set; }
    }
}