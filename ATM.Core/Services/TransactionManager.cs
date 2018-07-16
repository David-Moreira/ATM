using ATM.Core.Entities;
using ATM.Core.Interfaces;
using ATM.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Services
{
    class TransactionManager : ServiceBase<Transaction> , ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;

        public TransactionManager(ITransactionRepo transactionRepo) : base (transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }
    }
}
