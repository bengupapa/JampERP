using System;
using System.Web;

namespace JAMP.Models
{
    public class EntityUpdate
    {
        // Primary key
        public int EntityUpdateID { get; set; }

        // Attributes
        public string Entity { get; set; }
        public int UpdateNumber { get; set; }

        // Foreign key
        public int DeviceID { get; set; }

        // Navigation property
        public virtual Device Device { get; set; }
    }
}