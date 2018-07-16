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
    public class BankAccountManager : ServiceBase<BankAccount>, IBankAccountService
    {
        private readonly IBankAccountRepo _bankAccountRepo;

        public BankAccountManager(IBankAccountRepo bankAccountRepo) : base (bankAccountRepo)
        {
            _bankAccountRepo = bankAccountRepo;
        }

    }
}
