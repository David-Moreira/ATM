using ATM.Core.Entities;
using System.Data.Entity;

namespace ATM.Infrastructure.Data
{
    public class ATMDBContext : DbContext
    {
        public ATMDBContext()
            : base("ATMdb")
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}