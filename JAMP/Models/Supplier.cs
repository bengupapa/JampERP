using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using JAMP.Models;

namespace JAMP.Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.Orders = new HashSet<Order>();
            this.Archived = false;
        }

        // Primary key
        public int SupplierID { get; set; }

        // Attributes
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string SupplierAddress { get; set; }
        [Required]
        public string SupplierCity { get; set; }
        [Required]
        public string SupplierPostCode { get; set; }
        [Required]
        public string SupplierEmail { get; set; }
        [Required]
        public string SupplierContact { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public bool Archived { get; set; }
        public string ArchivedDate { get; set; }
        
        // Foreign key
        public int BusinessID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation property
        public virtual Business Business { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SupplierAccount SupplierAccount { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}