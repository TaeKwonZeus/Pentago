using System.ComponentModel.DataAnnotations;

namespace Pentago.Areas.Identity.Pages.Account.Models
{
    public class LoginInputModel
    {
        [Required] public string Username { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        public bool RememberMe { get; init; }
    }
}