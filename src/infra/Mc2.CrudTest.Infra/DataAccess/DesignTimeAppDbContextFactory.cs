using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infra.DataAccess
{
    public class DesignTimeDatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=CustomerDataBase;User Id=sa;Password=$tr0ngS@P@ssw0rd02");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
