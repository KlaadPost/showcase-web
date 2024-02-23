using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Showcase.Web.Models;

namespace Showcase.Web.Data;

public class ShowcaseWebContext(DbContextOptions<ShowcaseWebContext> options) : IdentityDbContext<ShowcaseUser>(options)
{
    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ShowcaseUser>()
            .HasMany(u => u.Messages)
            .WithOne()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedRoles(builder);
    }

    private void SeedRoles(ModelBuilder builder)
    {
        string[] roleNames = { "Administrator", "Moderator" };

        foreach (var roleName in roleNames)
        {
            var role = new IdentityRole { Name = roleName, NormalizedName = roleName.ToUpperInvariant() };
            builder.Entity<IdentityRole>().HasData(role);
        }
    }
}
