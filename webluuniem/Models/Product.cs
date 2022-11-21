using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace webluuniem.Models
{
    [Table("Product")]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        [DisplayName("ID")]
        public int ProductID { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
        [DisplayName("ID Danh mục")]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category category { get; set; }
        [DisplayName("Đơn giá")]
        public int Price { get; set; }
        [DisplayName("Giảm giá")]
        [Range(0, 99)]
        public int Discount { set; get; }

        [MaxLength(200)]
        [DisplayName("Mô tả ngắn")]
        public string DescribeShort { get; set; }
        [DisplayName("Chi tiết")]
        public string DescribeFull { get; set; }
        [DisplayName("ID Thương hiệu")]
        public int  BrandID { get; set; }
        [ForeignKey("BrandID")]
        public Brand Brand { get; set; }
        [DisplayName("Ảnh")]
        public string Image { get; set; }
        [DisplayName("Số lượng")]

        
        public int Amount { get; set; }
        [DisplayName("Xóa tạm")]


        public bool Deleted { get; set; } = false;
        public string Slug { get; set; }

    }
}