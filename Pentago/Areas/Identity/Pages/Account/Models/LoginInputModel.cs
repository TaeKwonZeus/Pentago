using System.ComponentModel.DataAnnotations;

namespace Pentago.Areas.Identity.Pages.Account.Models
{
    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}