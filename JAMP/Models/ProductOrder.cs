//Addded Namespaces
using System.ComponentModel.DataAnnotations;

namespace JAMP.Models
{
    public class ProductOrder
    {
        // Primary key
        public int ProductOrderID { get; set; }

        // Foreign key
        public int ProductID { get; set; }
        public int OrderID { get; set; }

        // Attributes
        public int QuantityOrdered { get; set; }
        public int QuantityDelivered { get; set; }
        public decimal CostPrice { get; set; }

        // Navigation property
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}