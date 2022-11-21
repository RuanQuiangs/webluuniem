using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace webluuniem.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int PostID { get; set; }

        [DisplayName("Tiêu đề")]
        public string PostTitle { get; set; }

        [DisplayName("Nội dung")]

        public string PostText { get; set; }
        [DisplayName("Ảnh")]
        public string Image { get; set; }
        [DisplayName("Mã người dùng")]
        public int UserID { get; set; }

        public UserModel User { get; set; }

        public string Slug { get; set; }

        [DisplayName("Thời gian đăng")]
        public DateTime CreateDate { get; set; }

        public bool Deleted { get; set; }
    }
}