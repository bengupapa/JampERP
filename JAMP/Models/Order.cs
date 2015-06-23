using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using JAMP.Models;


namespace JAMP.Models
{
    public class Order
    { 
        public Order()
        {
            // Defaults
            this.Archived = false;

            // Hash lists
            this.ProductOrders = new HashSet<ProductOrder>();
            this.Deliveries = new HashSet<Delivery>();
        }
        // Primary key
        public int OrderID { get; set; }

        // Attributes
        public string CreatedDate { get; set; }
        [Required]
        public string DateDue { get; set; }
        [Required]
        public decimal TotalCost { get; set; }
        [Required]
        public string Completed { get; set; }
        public string Notes { get; set; }
        public string ArchivedDate { get; set; }
        public bool Archived { get; set; }

        // Foreign key
        public int SupplierID { get; set; }
        public int BusinessID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation property
        public virtual Business Business { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}