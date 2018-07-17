using System.ComponentModel.DataAnnotations;

namespace ATM.Models
{
    public abstract class StandardTransactionViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        public int Amount { get; set; }

        [Required]
        public int accountNumber { get; set; }
    }

    public class TransactionViewModel : StandardTransactionViewModel { } //Withdraw | Deposit

    public class TransferFundsViewModel : StandardTransactionViewModel //Transfer funds | Payment
    {
        [Required]
        public int recipientAccountNumber { get; set; }
    }
}