using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class TransactionRepository : EfRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
