using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace JAMP.Models
{
    public class SupplierAccount
    {
        public SupplierAccount()
        {
            this.AmountOwed = 0;
            this.SupplierPayments = new HashSet<SupplierPayment>();
            this.Archived = false;
        }

        // Primary key
        [Key()]
        public int SupplierID { get; set; }

        // Attributes
        [Required]
        public string AccountName { get; set; }
        [Required]
        public double AmountOwed { get; set; }
        [Required]
        public double CreditLimit { get; set; }
        [Required]
        public string PaymentDate { get; set; }
        public string Comments { get; set; }
        public bool Archived { get; set; }

        // Navigation property
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<SupplierPayment> SupplierPayments { get; set; }
    }
}