using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pentago.API.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        public string NormalizedUsername { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        public string ApiKeyHash { get; set; }
        
        [Required]
        public int GlickoRating { get; set; }
        
        [Required]
        public float GlickoRd { get; set; }
        
        public IEnumerable<Game> GamesWhite { get; set; }
        
        public IEnumerable<Game> GamesBlack { get; set; }
    }
}