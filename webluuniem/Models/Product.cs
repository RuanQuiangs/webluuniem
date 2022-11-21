using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webluuniem.Models
{
    [Table("Product")]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int CategoryID { get; set; }

        public Category category { get; set; }

        public int Price { get; set; }

        public int Discount { set; get; }

        [MaxLength(200)]
        public string DescribeShort { get; set; }
                
        
        public string DescribeFull { get; set; }

        public string BrandID { get; set; }

       
        public Brand Brand { get; set; }

        public string Image { get; set; }

        public int Amount { get; set; }

        public bool Deleted { get; set; } = false;

    }
}