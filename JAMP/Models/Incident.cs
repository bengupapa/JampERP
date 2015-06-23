using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class Incident
    {
        public Incident() 
        {
            this.ProductIncidents = new HashSet<ProductIncident>();
        }

        // Primary key
        public int IncidentID { get; set; }

        // Attributes
        public string CreatedDate { get; set; }
        public string Notes { get; set; }
        [Required]
        public string Type { get; set; }    //theft, damage, expired, removed, stock take

        // Foreign key
        public int EmployeeID { get; set; }
        public int BusinessID { get; set; }

        // Navigation property
        public virtual Business Business { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ProductIncident> ProductIncidents { get; set; }
    }
}