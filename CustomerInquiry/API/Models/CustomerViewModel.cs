using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            Transactions = new List<TransactionViewModel>();
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public List<TransactionViewModel> Transactions { get; set; }
    }
}
