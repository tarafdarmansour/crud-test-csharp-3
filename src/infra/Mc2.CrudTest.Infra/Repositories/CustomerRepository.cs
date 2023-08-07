using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.Core.Domain.Entity;
using Mc2.CrudTest.Core.Domain.Repositories;
using Mc2.CrudTest.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Mc2.CrudTest.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public CustomerRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task CreateAsync(CustomerEntity customer)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Customers.Add(customer);

            _ = await context.SaveChangesAsync();
        }

        public IQueryable<CustomerEntity> Get(Expression<Func<CustomerEntity, bool>> predict)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return context.Customers.AsNoTracking().Where(predict);
        }
    }
}
