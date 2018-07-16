using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Core.Entities
{
    public class BankAccount
    {

        public int AccountNumber { get; set; }

        public int Balance { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public string UserID { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}