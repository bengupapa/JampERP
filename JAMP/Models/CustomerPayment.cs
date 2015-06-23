using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class CustomerPayment
    {
        // Primary key
        public int CustomerPaymentID { get; set; }

        // Attributes
        public string CreatedDate { get; set; }
        [Required]
        public decimal AmountPaid { get; set; }
        [Required]
        public string Reference { get; set; }

        // Foreign key
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation property
        public virtual CustomerAccount CustomerAccount { get; set; }
        public virtual Employee Employee { get; set; }
    }
}