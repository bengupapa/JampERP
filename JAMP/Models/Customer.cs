using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
            this.Archived = false;

        }

        // Primary key
        public int CustomerID { get; set; }

        // Attributes
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerSurname { get; set; }
        [Required]
        public string CustContacts { get; set; }
        [Required]
        public string CustEmail { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PostCode { get; set; }
        public string CreatedDate { get; set; }
        public bool Archived { get; set; }
        public string ArchivedDate { get; set; }
       
        // Foreign key
        public int BusinessID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation property
        public virtual Business Business { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual CustomerAccount CustomerAccount { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}