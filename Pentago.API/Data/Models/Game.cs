using System.ComponentModel.DataAnnotations;

namespace Pentago.API.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public User White { get; set; }
        
        [Required]
        public User Black { get; set; }
        
        [Required]
        public string GameData { get; set; }
    }
}