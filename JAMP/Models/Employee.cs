using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class Employee
    {
        public Employee()
        {
            // Defaults
            this.LoginCount = 0;
            this.Archived = false;
            this.ImageLocation = "../../Images/ProfilePictures/photo.jpg";

            // Hash sets
            this.Products = new HashSet<Product>();
            this.Suppliers = new HashSet<Supplier>();
            this.Sales = new HashSet<Sale>();
            this.Orders = new HashSet<Order>();
            this.Incidents = new HashSet<Incident>();
            this.Deliveries = new HashSet<Delivery>();
            this.CustomerPayments = new HashSet<CustomerPayment>();
            this.SupplierPayments = new HashSet<SupplierPayment>();
            this.Devices = new HashSet<Device>(); 
        }

        // Primary key
        public int EmployeeID { get; set; }

        // Attributes
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Contacts { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string CreatedDate { get; set; }
        [Required]
        public string Password { get; set; }
        public string ImageLocation { get; set; }
        public bool Online { get; set; }
        public string LastSeen { get; set; }
        public int LoginCount { get; set; }
        public bool Archived { get; set; }
        public string ArchivedDate { get; set; }

        // Foreign key
        public int BusinessID { get; set; }

        // Navigation property
        public virtual SettingUser Settings { get; set; }
        public virtual Business Business { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<CustomerPayment> CustomerPayments { get; set; }
        public virtual ICollection<SupplierPayment> SupplierPayments { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}