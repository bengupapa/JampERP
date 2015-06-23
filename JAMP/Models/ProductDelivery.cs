using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JAMP.Models
{
    public class ProductDelivery
    {
        // Primary key
        public int ProductDeliveryID { get; set; }

        // Foreign key
        public int ProductID { get; set; }
        public int DeliveryID { get; set; }

        // Attributes
        public int QuantityDelivered { get; set; }

        // Navigation property
        public virtual Product Product { get; set; }
        public virtual Delivery Delivery { get; set; }
    }
}