using ATM.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Facade
{
    public class OperationManager : IOperationService
    {
        public void Deposit(int amount)
        {
            throw new NotImplementedException();
        }

        public void Payment(string recipientAccountNumber, int amount)
        {
            throw new NotImplementedException();
        }

        public string PrintStatement()
        {
            throw new NotImplementedException();
        }

        public void QuickCash()
        {
            throw new NotImplementedException();
        }

        public void TransferFunds(string accountNumber, int amount)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
