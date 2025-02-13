﻿using System.Linq.Expressions;
using Mc2.CrudTest.Core.Domain.Entity;

namespace Mc2.CrudTest.Core.Domain.Repositories;

public interface ICustomerRepository
{
    Task CreateAsync(CustomerEntity customer);
    IList<CustomerEntity> Get(Expression<Func<CustomerEntity, bool>> predict);
    IList<CustomerEntity> GetAll();
}