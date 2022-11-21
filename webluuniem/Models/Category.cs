using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace webluuniem.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string Slug { get; set; }

        public string Avatar { get; set; }

        public bool ShowOnHomePage { get; set; } = true;

        public int DisplayOrder { get; set; }

        public DateTime CreateOnUtc { get; set; } = DateTime.UtcNow;

        public DateTime UpdateOnUtc { get; set; } = DateTime.UtcNow;

        public bool Deleted { get; set; } = false;
    }
}