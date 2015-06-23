using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JAMP.Models
{
    public class Device
    {
        // Primary key
        public int DeviceID { get; set; }

        // Attributes
        public string Number { get; set; }

        // Foreign key
        public int EmployeeID { get; set; }

        // Navigation property
        public virtual Employee Employee { get; set; }
        public virtual ICollection<EntityUpdate> Updates { get; set; }
    }
}