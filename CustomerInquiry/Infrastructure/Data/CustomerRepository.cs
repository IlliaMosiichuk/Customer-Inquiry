using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
