using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlexConMan_Monolith.Models;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<ConferenceCenter> ConferenceCenters { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Conference> Conferences { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Contingent> Contingents { get; set; }
    public DbSet<Presentation> Presentations { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Session>()
            .HasOne(s => s.Conference)
            .WithMany(c => c.Sessions)
            .HasForeignKey(s => s.ConferenceId)
            .OnDelete(DeleteBehavior.Cascade); // Only one cascade

        modelBuilder.Entity<Session>()
            .HasOne(s => s.Room)
            .WithMany(r => r.Sessions)
            .HasForeignKey(s => s.RoomId)
            .OnDelete(DeleteBehavior.Restrict); // No cascade

        modelBuilder.Entity<Session>()
            .HasOne(s => s.Presentation)
            .WithMany(p => p.Sessions)
            .HasForeignKey(s => s.PreId)
            .OnDelete(DeleteBehavior.Restrict); // No cascade

        modelBuilder.Entity<Session>()
            .HasOne(s => s.Contingent)
            .WithMany(cg => cg.Sessions)
            .HasForeignKey(s => s.ContingentId)
            .OnDelete(DeleteBehavior.Restrict); // No cascade

        modelBuilder.Entity<Room>()
            .HasOne(r => r.ConferenceCenter)
            .WithMany(cc => cc.Rooms)
            .HasForeignKey(r => r.CcId)
            .OnDelete(DeleteBehavior.Cascade); // Only one cascade
    }

}
