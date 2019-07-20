using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Customer GetByEmail(string email)
        {
            return GetAll(c => c.Email.ToLower() == email.ToLower())
                .FirstOrDefault();
        }

        public Customer GetByEmailAndId(string email, long id)
        {
            return GetAll(c => c.Email.ToLower() == email.ToLower() && c.Id == id)
                .FirstOrDefault();
        }
    }
}
