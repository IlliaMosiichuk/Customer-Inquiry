using ApplicationCore.Entities;
using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static void Seed(ApplicationDbContext dbContext)
        {
            try
            {
                if (!dbContext.Customers.Any())
                {
                    var testCustomers = GetTestCustomers();
                    dbContext.Customers.AddRange(testCustomers);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //TODO: add logging
            }
            
        }

        private static IEnumerable<Customer> GetTestCustomers()
        {
            return new List<Customer>()
            {
                new Customer()
                {
                    Name = "Firstname1 Lastname1",
                    Email = "user1@domain.com",
                    Mobile = "0123456789",
                },
                new Customer()
                {
                    Name = "Firstname2 Lastname2",
                    Email = "user2@domain.com",
                    Mobile = "0223456789",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            Date = new DateTime(2018, 3, 31, 21, 23, 0),
                            Amount = 1234.56m,
                            Currency = "USD",
                            Status = ETransactionStatus.Success
                        }
                    }
                },
                new Customer()
                {
                    Name = "Firstname3 Lastname3",
                    Email = "user3@domain.com",
                    Mobile = "0323456789",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            Date = new DateTime(2018, 3, 31, 21, 23, 0),
                            Amount = 1234.56m,
                            Currency = "EUR",
                            Status = ETransactionStatus.Success
                        },
                        new Transaction()
                        {
                            Date = new DateTime(2018, 3, 31, 21, 23, 0),
                            Amount = 0.56m,
                            Currency = "THB",
                            Status = ETransactionStatus.Failed
                        }
                    }
                },
            };
        }
    }
}
