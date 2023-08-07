using Mc2.CrudTest.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infra.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CustomerEntity> Customers { get; set; }
}