using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JAMP.Models
{
    public class SalesItem
    {
        // Primary key
        public int SalesItemID { get; set; }

        // Foreign Keys
        public int SaleID { get; set; }
        public int ProductID { get; set; }

        // Attributes
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
       

        // Navigation property
        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }
    }
}