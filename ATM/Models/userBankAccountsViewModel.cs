using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class userBankAccountsViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string _userBankAccount;
        public string userBankAccount { get { return _userBankAccount; } }

  

        public string getuserBankAccount (string userID)
        {

            var bankAccount = from account in db.BankAccounts
                               where account.UserID == userID
                               select account.AccountNumber;

            return _userBankAccount = bankAccount.First(); ;
        }
    }
}