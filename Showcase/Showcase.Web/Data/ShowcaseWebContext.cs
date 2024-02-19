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
    }
}
