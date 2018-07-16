using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class Transaction
    {

        public int ID { get; set; }

        [Required]

        public string Amount { get; set; }

        [Required]
        public string Recipient { get; set; }

        [Required]
        [ForeignKey("BankAccount")]
        public string AccountNumber { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
}