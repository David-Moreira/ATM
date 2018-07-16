using System;
using System.Linq;

namespace ATM.Core.Entities
{
    public class OperationsModel
    {
        private BankAccount _bankAccount;
        private Transaction _transaction = new Transaction();
        private ApplicationDbContext db = new ApplicationDbContext();

        public OperationsModel(string accountNumber)
        {
            var bankAccount = db.BankAccounts.Single(x => x.AccountNumber == accountNumber);
            _bankAccount = bankAccount;
            _transaction.AccountNumber = accountNumber;
        }

        public void Withdraw(int amount)
        {
            if (_bankAccount.Balance - amount > 0)
            {
                _bankAccount.Balance -= amount;
                _transaction.Amount = "-" + amount.ToString();
                _transaction.Recipient = _bankAccount.AccountNumber;
                db.Transactions.Add(_transaction);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            };

            //Alert user
        }

        public void Deposit(int amount)
        {
            _bankAccount.Balance += amount;
            _transaction.Amount = "+" + amount.ToString();
            _transaction.Recipient = _bankAccount.AccountNumber;
            db.Transactions.Add(_transaction);
            db.SaveChanges();
        }

        public void Payment(string recipientAccountNumber, int amount)
        {
            var recipientAcc = db.BankAccounts.Single(x => x.AccountNumber == recipientAccountNumber);

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

                db.Transactions.Add(_transaction);
                db.Transactions.Add(recipientTransaction);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            };
        }

        public void TransferFunds(string accountNumber, int amount)
        {
            var recipientAcc = db.BankAccounts.Single(x => x.AccountNumber == accountNumber);

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

                db.Transactions.Add(_transaction);
                db.Transactions.Add(recipientTransaction);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            };
        }

        public void QuickCash()
        {
            if (_bankAccount.Balance - 10 > 0)
            {
                _bankAccount.Balance -= 10;
                _transaction.Amount = "-10";
                _transaction.Recipient = _bankAccount.AccountNumber;
                db.Transactions.Add(_transaction);
                db.SaveChanges();
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
    }
}