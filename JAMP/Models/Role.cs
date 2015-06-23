using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class Role
    {
        // Primary key
        public int RoleID { get; set; }

        // Attributes
        [Required]
        public string RoleName { get; set; }
    }
}