using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Showcase.Web.Models;

namespace Showcase.Web.Data;

public class ShowcaseWebContext : IdentityDbContext<ShowcaseUser>
{
    public DbSet<ChatMessage> ChatMessages { get; set; }

    public ShowcaseWebContext(DbContextOptions<ShowcaseWebContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ShowcaseUser>()
            .HasMany(u => u.Messages)
            .WithOne()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
