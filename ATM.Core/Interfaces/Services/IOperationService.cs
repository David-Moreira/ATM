using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Interfaces.Services
{
    public interface IOperationService
    {

        void Withdraw(int accountNumber, int amount);


        void Deposit(int accountNumber, int amount);


        void Payment(int accountNumber, int recipientAccountNumber, int amount);


        void TransferFunds(int accountNumber, int recipientAccountNumber, int amount);


        void QuickCash(int accountNumber);


        string PrintStatement(int accountNumber);
       
    }
}
