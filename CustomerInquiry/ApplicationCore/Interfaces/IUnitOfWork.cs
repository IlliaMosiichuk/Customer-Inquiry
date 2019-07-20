using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        ITransactionRepository Transactions { get; }

        int Commit();
    }
}
