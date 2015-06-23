using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Data.SqlClient;

namespace JAMP.Models
{
    public class CustomerAccount
    {
        public CustomerAccount()
        {
            this.CustomerPayments = new HashSet<CustomerPayment>();
            this.Archived = false;
        }

        // Primary key
        [Key()]
        public int CustomerID { get; set; }

        // Attributes
        [Required]
        public string AccountName { get; set; }
        [Required]
        public double AmountOwing { get; set; }
        [Required]
        public double CreditLimit { get; set; }
        public string PaymentDate { get; set; }
        public string Comments { get; set; }
        public bool Archived { get; set; }

        // Navigation property
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CustomerPayment> CustomerPayments { get; set; }
    }
}

