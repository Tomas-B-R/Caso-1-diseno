using DuaBusiness.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DuaBusiness.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProcessingJob> ProcessingJobs => Set<ProcessingJob>();

    public DbSet<DuaTemplate> Templates => Set<DuaTemplate>();

    public DbSet<NotificationSubscription> NotificationSubscriptions => Set<NotificationSubscription>();

    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
}
