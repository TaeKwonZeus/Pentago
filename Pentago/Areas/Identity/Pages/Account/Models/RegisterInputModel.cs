using System.ComponentModel.DataAnnotations;

namespace Pentago.Areas.Identity.Pages.Account.Models
{
    public class RegisterInputModel
    {
        [Required]
        [StringLength(24, ErrorMessage = "The username's length should be between 3 and 24 characters long.",
            MinimumLength = 3)]
        public string Username { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The password must be at least 6 and at max 100 characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}