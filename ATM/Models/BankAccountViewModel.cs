using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Infrastructure.Entities
{


    public class BankAccountViewModel
    {
        [Key]
        [Display(Name = "Account #")]
        [Required]
        //[RegularExpression(@"\d{6,10}", ErrorMessage = "Account # must be between 6 and 10 numbers.")]
        public string AccountNumber { get; set; }


        [DataType(DataType.Currency)]
        public int Balance { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public string UserID { get; set; }

    }
}