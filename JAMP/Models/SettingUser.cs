using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace JAMP.Models
{
    public class SettingUser
    {
        public SettingUser() // Default settings
        {
            this.AutoUpdate = "On";
            this.Descriptions = "On";
        }

        // Primary key
        [Key()]
        public int EmployeeID { get; set; }

        // Attributes
        public string AutoUpdate { get; set; }
        public string Descriptions { get; set; }

        // Navigation property
        public virtual Employee Employee { get; set; }
        
    }
}