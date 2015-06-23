//Addded Namespaces
using System.ComponentModel.DataAnnotations;

namespace JAMP.Models
{
    public class ProductIncident
    {
        // Primary key
        public int ProductIncidentID { get; set; }

        // Foreign key
        public int ProductID { get; set; }
        public int IncidentID { get; set; }

        // Attributes
        public int Quantity { get; set; }
        public bool Removed { get; set; }          // Removed or Added 

        // Navigation property
        public virtual Product Product { get; set; }
        public virtual Incident Incident { get; set; }
    }
}