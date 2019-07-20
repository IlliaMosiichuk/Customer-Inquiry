using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Transactions = new List<Transaction>();
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
