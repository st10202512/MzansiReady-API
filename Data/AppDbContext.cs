using Microsoft.EntityFrameworkCore;
using MzansiReady.Api.Models;

namespace MzansiReady.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<SavedLocation> SavedLocations => Set<SavedLocation>();
    public DbSet<Alert> Alerts => Set<Alert>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<SavedLocation>()
            .HasOne(l => l.User)
            .WithMany(u => u.SavedLocations)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}