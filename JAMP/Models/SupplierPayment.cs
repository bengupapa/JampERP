using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class SupplierPayment
    {
        // Primary key
        public int SupplierPaymentID { get; set; }

        // Attributes
        public string CreatedDate { get; set; }
        [Required]
        public decimal AmountPaid { get; set; }
        [Required]
        public string Reference { get; set; }   

        // Foreign key
        public int SupplierID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation property
        public virtual SupplierAccount SupplierAccount { get; set; }
        public virtual Employee Employee { get; set; }
    }
}