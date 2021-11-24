using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pentago.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(b =>
            {
                b.HasKey(u => u.Id);

                b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

                b.ToTable("Users");

                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);
            });
        }
        */
    }
}