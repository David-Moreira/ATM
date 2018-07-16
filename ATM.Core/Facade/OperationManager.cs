using ATM.Core.Entities;
using ATM.Core.Interfaces;
using ATM.Core.Interfaces.Services;
using ATM.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Facade
{
    public class OperationManager : IOperationService
    {

        private readonly IBankAccountService _bankManager;
        private readonly ITransactionService _transactionManager;

        private BankAccount _bankAccount;
        private Transaction _transaction;

        public OperationManager(int accountNumber, IBankAccountService bankManager, ITransactionService transactionManager)
        {
            _bankManager = bankManager;
            _transactionManager = transactionManager;
            _bankAccount = _bankManager.GetById(accountNumber);
            _transaction = new Transaction();
            _transaction.AccountNumber = accountNumber;
        }
        
        //Have to rethink this with transactions
        public void Deposit(int amount)
        {
            _bankAccount.Balance += amount;
            _transaction.Amount = $"+ {amount}";
            _transaction.Recipient = _bankAccount.AccountNumber;
            _bankManager.Update(_bankAccount);
            _transactionManager.Update(_transaction);
        }

        public void Payment(int recipientAccountNumber, int amount)
        {
            var recipientAcc = _bankManager.GetById(recipientAccountNumber);

            if (_bankAccount.Balance - amount > 0)
            {
                _bankAccount.Balance -= amount;
                _transaction.Amount = "-" + amount.ToString();
                _transaction.Recipient = _bankAccount.AccountNumber;

                recipientAcc.Balance += amount;

                Transaction recipientTransaction = new Transaction();
                recipientTransaction.AccountNumber = _bankAccount.AccountNumber;
                recipientTransaction.Amount = "+" + amount.ToString();
                recipientTransaction.Recipient = recipientAcc.AccountNumber;

                _bankManager.Update(recipientAcc);
                _bankManager.Update(_bankAccount);
                
                _transactionManager.Add(_transaction);
                _transactionManager.Add(recipientTransaction);

            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            };
        }

        public string PrintStatement()
        {
            return string.Format("Conta: {0} \nFull Name: {1}\nBalance: {2}", _bankAccount.AccountNumber, _bankAccount.FullName, _bankAccount.Balance);
        }

        public void QuickCash()
        {
            if (_bankAccount.Balance - 10 > 0)
            {
                _bankAccount.Balance -= 10;
                _transaction.Amount = "-10";
                _transaction.Recipient = _bankAccount.AccountNumber;

                _bankManager.Update(_bankAccount);
                _transactionManager.Add(_transaction);
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            };
        }

        public void TransferFunds(int accountNumber, int amount)
        {
            var recipientAcc = _bankManager.GetById(accountNumber);

            if (_bankAccount.Balance - amount > 0)
            {
                _bankAccount.Balance -= amount;
                recipientAcc.Balance += amount;
                _transaction.Amount = "-" + amount.ToString();
                _transaction.Recipient = _bankAccount.AccountNumber;

                Transaction recipientTransaction = new Transaction();
                recipientTransaction.AccountNumber = _bankAccount.AccountNumber;
                recipientTransaction.Amount = "+" + amount.ToString();
                recipientTransaction.Recipient = recipientAcc.AccountNumber;

                _bankManager.Update(recipientAcc);
                _bankManager.Update(_bankAccount);

                _transactionManager.Add(_transaction);
                _transactionManager.Add(recipientTransaction);
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            };
        }


        public void Withdraw(int amount)
        {
            if (_bankAccount.Balance - amount > 0)
            {
                _bankAccount.Balance -= amount;
                _transaction.Amount = "-" + amount.ToString();
                _transaction.Recipient = _bankAccount.AccountNumber;

                _bankManager.Update(_bankAccount);
                _transactionManager.Add(_transaction);
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            };
        }



    }
}

