using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace ATM.Models
{
    public abstract class StandardTransactionViewModel
    {

        [Required]
        [DataType(DataType.Currency)]
        public int Amount { get; set;}

        [Required]
        [RegularExpression(@"\d{6,10}")]
        public string accountNumber {get;set;}
    }

    public class TransactionViewModel : StandardTransactionViewModel { } //Withdraw | Deposit

    public class TransferFundsViewModel : StandardTransactionViewModel //Transfer funds | Payment
    {
        [Required]
        [RegularExpression(@"\d{6,10}")]
        public string recipientAccountNumber { get; set; }

    }


}