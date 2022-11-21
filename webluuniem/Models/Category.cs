using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("ID")]
        public int CategoryID { get; set; }
        [DisplayName("Tên")]
        public string CategoryName { get; set; }

        public string Slug { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Ảnh")]

        public string Avatar { get; set; }
        [DisplayName("Hiển thị trên home")]
        public bool ShowOnHomePage { get; set; } = true;
        [DisplayName("Thứ tự hiển thị")]
        public int DisplayOrder { get; set; }
        [DisplayName("Thời gian tạo")]
        public DateTime CreateOnUtc { get; set; } = DateTime.UtcNow;
        [DisplayName("Thời gian cập nhật")]
        public DateTime UpdateOnUtc { get; set; } = DateTime.UtcNow;
        [DisplayName("Xóa tạm")]
        public bool Deleted { get; set; } = false;
    }
}