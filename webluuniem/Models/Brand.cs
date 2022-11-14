using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webluuniem.Models
{
    [Table("Brand")]    
    
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BrandID { get; set; }

        public string BrandName { get; set; }

        public string Slug { get; set; }

        public string Avatar { get; set; }

        public bool ShowOnHomePage  { get; set; } = true;

        public int DisplayOrder { get; set; }

        public string Country { get; set; }

        public DateTime CreateOnUtc { get; set; } = DateTime.UtcNow;

        public DateTime UpdateOnUtc { get; set; }
        
        public bool Deleted { get; set; }
    }
}