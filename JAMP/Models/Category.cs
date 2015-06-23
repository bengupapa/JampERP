using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMP.Models
{
    public class Category
    {
        public Category() 
        {
            this.Archived = false;
            this.ProductList = new HashSet<Product>();
        }

        // Primary key
        public int CategoryID { get; set; }

        // Attributes
        [Required]
        public string CategoryName { get; set; }
        public string CreatedDate { get; set; }
        public bool Archived { get; set; }
        public string ArchivedDate { get; set; }

        // Foreign key
        public int BusinessID { get; set; }

        // Navigation property
        public virtual Business Business { get; set; }
        public virtual ICollection<Product> ProductList { get; set; }
    }
}