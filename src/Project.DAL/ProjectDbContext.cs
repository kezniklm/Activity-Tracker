using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;

namespace Project.DAL
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions contextOptions)
            : base(contextOptions) { }

        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<UserProjectEntity> UsersProjects => Set<UserProjectEntity>();
    }
}