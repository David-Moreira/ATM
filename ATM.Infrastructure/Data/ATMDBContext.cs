using ATM.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ATM.Infrastructure.Data
{
    public class ATMDBContext : DbContext
    {
        //EF 6.0
        //public ATMDBContext()
        //    : base("ATMdb")
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ATMdb"].ToString());
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}