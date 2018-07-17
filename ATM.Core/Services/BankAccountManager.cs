using ATM.Core.Entities;
using ATM.Core.Interfaces;
using ATM.Core.Interfaces.Services;
using System;
using System.Linq;

namespace ATM.Core.Services
{
    public class BankAccountManager : ServiceBase<BankAccount>, IBankAccountService
    {
        private readonly IBankAccountRepo _bankAccountRepo;

        public BankAccountManager(IBankAccountRepo bankAccountRepo) : base(bankAccountRepo)
        {
            _bankAccountRepo = bankAccountRepo;
        }

        public override BankAccount Add(BankAccount newAcc)
        {
            if (String.IsNullOrEmpty(newAcc.UserID))
                throw new Exception("No user associated with account.");

            newAcc.AccountNumber = int.Parse(GetAll().Count().ToString().PadLeft(10, '0'));
            return base.Add(newAcc);
        }

        public BankAccount GetByUserId(string id)
        {
            return _bankAccountRepo.GetSingle(x => x.UserID == id);
        }
    }
}