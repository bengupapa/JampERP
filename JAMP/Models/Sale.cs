//Addded Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class Sale
    {
        public Sale()
        {
            this.SalesItems = new HashSet<SalesItem>();
        }

        // Primary key
        public int SaleID { get; set; }

        // Attributes
        public string CreatedDate { get; set; }
        [Required]
        public decimal AmountCharged { get; set; }
        [Required]
        public decimal AmountReceived { get; set; }
        [Required]
        public decimal Change { get; set; }
        [Required]
        public bool Credit { get; set; }

        // Foreign key
        public int? CustomerID { get; set; }
        public int BusinessID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation property
        public virtual Customer Customer { get; set; }
        public virtual Business Business { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<SalesItem> SalesItems { get; set; }
    }
}