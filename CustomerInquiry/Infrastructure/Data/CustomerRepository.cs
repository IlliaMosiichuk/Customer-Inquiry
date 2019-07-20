using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public override IEnumerable<Customer> GetAll(Func<Customer, bool> predicate = null)
        {
            if (predicate == null)
            {
                return _dbSet.Include(c => c.Transactions).ToList();
            }

            return _dbSet.Include(c => c.Transactions).Where(predicate).ToList();
        }

        public Customer GetByEmail(string email)
        {
            return _dbSet.Include(c => c.Transactions)
                .Where(c => c.Email.ToLower() == email.ToLower())
                .FirstOrDefault();
        }

        public Customer GetByEmailAndId(string email, long id)
        {
            return  _dbSet.Include(c => c.Transactions)
                .Where(c => c.Email.ToLower() == email.ToLower() && c.Id == id)
                .FirstOrDefault();
        }

        public override Customer GetById(long id)
        {
            return _dbSet.Include(c => c.Transactions)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
