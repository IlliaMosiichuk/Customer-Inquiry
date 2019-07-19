using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Transaction : BaseEntity
    {
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public ETransactionStatus Status { get; set; }

        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }


}
