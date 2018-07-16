using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Core.Entities
{
    public class Transaction
    {
        public int ID { get; set; }

        public string Amount { get; set; }

        public int Recipient { get; set; }

        [ForeignKey("BankAccount")]
        public int AccountNumber { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}