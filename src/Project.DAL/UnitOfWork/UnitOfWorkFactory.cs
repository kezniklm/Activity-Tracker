using Microsoft.EntityFrameworkCore;

namespace Project.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<ProjectDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<ProjectDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}
