using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class Business
    {
        public Business()
        {
            // List
            this.Employees = new List<Employee>();

            // Hash lists
            this.Categories = new HashSet<Category>();
            this.Products = new HashSet<Product>();
            this.Suppliers = new HashSet<Supplier>();
            this.Customers = new HashSet<Customer>();
            this.Sales = new HashSet<Sale>();
            this.Orders = new HashSet<Order>();
            this.Incidents = new HashSet<Incident>();

            // Defaults
            this.DisableSales = "Off";
        }

        // Primary key
        public int BusinessID { get; set; }
        
        // Attributes
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string BusinessStreet { get; set; }
        [Required]
        public string BusinessCity { get; set; }
        [Required]
        public string BusinessPostalCode { get; set; }
        [Required]
        public string BusinessEmail { get; set; }
        [Required]
        public string BusinessContacts { get; set; }
        [Required]
        public string CreatedDate { get; set; }
        public string DisableSales { get; set; }

        // Navigation property
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}