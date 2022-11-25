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

        public string Slug { get; set; }

        [DisplayName("Thời gian đăng")]
        public DateTime CreateDate { get; set; }

        public bool Deleted { get; set; }
    }

    [Table("Comment")]
    public class Comment 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        public int PostID { get; set; }

        public string Text { get; set; }

        public DateTime DateComment { get; set; } = DateTime.UtcNow;

        public int UserID { get; set; }

    }

    [Table("Review")]
    public class ReView 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReViewID { get; set; }

        public int UserID { get; set; }

        public byte Star { get; set; } = 5;

        public string Text { get; set; }

        public DateTime DateReview { get; set; } = DateTime.UtcNow;

    }

    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackID { get; set; }

        public string Name { get; set; }

        public string email { get; set; }

        public string text { get; set; }

        public DateTime DateFeedback { get; set; } = DateTime.UtcNow;
    }

  


}