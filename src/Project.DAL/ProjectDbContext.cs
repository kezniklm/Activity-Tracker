using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;

namespace Project.DAL;

public class ProjectDbContext : DbContext
{
    private readonly bool _seedDemoData;

    public ProjectDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
        : base(contextOptions) => _seedDemoData = seedDemoData;

    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<UserProjectEntity> UsersProjects => Set<UserProjectEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectEntity>()
            .HasMany<ActivityEntity>(i => i.Activities)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserEntity>()
            .HasMany<ActivityEntity>(i => i.Activities)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserProjectEntity>()
            .HasKey(i => new { i.UserId, i.ProjectId });

        modelBuilder.Entity<UserProjectEntity>()
            .HasOne<UserEntity>(i => i.User)
            .WithMany(i => i.Projects)
            .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<UserProjectEntity>()
            .HasOne<ProjectEntity>(i => i.Project)
            .WithMany(i => i.Users)
            .HasForeignKey(i => i.ProjectId);

        if (_seedDemoData)
        {
        }
    }
}
