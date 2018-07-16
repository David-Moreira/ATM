using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Interfaces.Services
{
    public interface IOperationService
    {

        void Withdraw(int amount);


        void Deposit(int amount);


        void Payment(string recipientAccountNumber, int amount);


        void TransferFunds(string accountNumber, int amount);


        void QuickCash();


        string PrintStatement();
       
    }
}
