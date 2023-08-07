using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infra.DataAccess;

public class DatabaseContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        _configureDbContext = configureDbContext;
    }

    public DatabaseContext CreateDbContext()
    {
        DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
        _configureDbContext(optionsBuilder);

        return new DatabaseContext(optionsBuilder.Options);
    }
}