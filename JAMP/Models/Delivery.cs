using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class Delivery
    {
        public Delivery()
        {
            this.ProductDeliveries = new HashSet<ProductDelivery>();
        }
        // Primary key
        public int DeliveryID { get; set; }

        // Attributes
        public string CreatedDate { get; set; }
        public string Notes { get; set; }

        // Foreign key
        public int OrderID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation property
        public virtual Order Order { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ProductDelivery> ProductDeliveries { get; set; }
        
    }
}