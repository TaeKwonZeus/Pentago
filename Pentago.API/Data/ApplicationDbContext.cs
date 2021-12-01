using Microsoft.EntityFrameworkCore;
using Pentago.API.Data.Models;

namespace Pentago.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.NormalizedUsername).IsUnique();

            builder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

            builder.Entity<Game>()
                .HasOne(g => g.White)
                .WithMany(u => u.GamesWhite);

            builder.Entity<Game>()
                .HasOne(g => g.Black)
                .WithMany(u => u.GamesBlack);
        }
    }
}