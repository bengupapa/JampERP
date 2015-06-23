//Addded Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class Product
    {
        public Product()
        {
            // Defaults
            this.Archived = false;
            this.Special = "No";

            // Hash Sets
            this.SalesItems = new HashSet<SalesItem>();
            this.ProductIncidents = new HashSet<ProductIncident>();
            this.ProductOrders = new HashSet<ProductOrder>();
            this.ProductDeliveries = new HashSet<ProductDelivery>();
        }

        // Primary key
        public int ProductID { get; set; }

        // Attributes
        [Required]
        public string BrandName { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        [Required]
        public decimal SellingPrice { get; set; }
        [Required]
        public decimal CostPrice { get; set; }
        [Required]
        public int ReorderLevel { get; set; }
        [Required]
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string ArchivedDate { get; set; }
        public bool Archived { get; set; }
        public string Special { get; set; }

        // Foreign key
        public int CategoryID { get; set; }
        public int BusinessID { get; set; }
        public int EmployeeID { get; set; }


        // Navigation property
        public virtual Business Business { get; set; }
        public virtual Category Category { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<SalesItem> SalesItems { get; set; }
        public virtual ICollection<ProductIncident> ProductIncidents { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<ProductDelivery> ProductDeliveries { get; set; }
    }
}