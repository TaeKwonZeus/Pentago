using Microsoft.AspNetCore.Identity;

namespace Pentago.Areas.Identity.Pages.Account.Models
{
    public class User : IdentityUser
    {
        public int Elo { get; set; }
    }
}