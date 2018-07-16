using System.ComponentModel.DataAnnotations;

namespace ATM.Models
{
    public abstract class StandardTransactionViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        public int Amount { get; set; }

        [Required]
        [RegularExpression(@"\d{6,10}")]
        public int accountNumber { get; set; }
    }

    public class TransactionViewModel : StandardTransactionViewModel { } //Withdraw | Deposit

    public class TransferFundsViewModel : StandardTransactionViewModel //Transfer funds | Payment
    {
        [Required]
        [RegularExpression(@"\d{6,10}")]
        public string recipientAccountNumber { get; set; }
    }
}