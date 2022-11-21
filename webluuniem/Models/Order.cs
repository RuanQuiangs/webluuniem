using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace webluuniem.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public Guid OrderID { get; set; }
        [DisplayName("ID sản phẩm")]
        public int ProductID { get; set; }

        public Product Product { get; set; }
        [DisplayName("ID người đăt")]
        public int UserID { get; set; }

        public UserModel User { get; set; }
        [DisplayName("Tên Order")]
        public string OrderName { get; set; }


        [DisplayName("Số Lượng")]
        public int Amount { get; set; }

        [Range(0, double.MaxValue)]
       
        [DisplayName("Thành tiền")]
        public decimal Price { get; set; }
        [DisplayName("Địa chỉ nhận")]
        public string AdrressOrder { get; set; }
        [DisplayName("Điện thoại người nhận")]
        public string PhoneOrder { get; set; }
        [DisplayName("Trạng thái đơn hàng")]
        public OderStatus OrderStatus { get; set; }

        [DisplayName("Thời gian tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [DisplayName("Thòi gian cập nhật")]
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

    }

    public enum OderStatus
    {
        giohang = 0,
        Dathang = 1,
        Xacnhan = 2,
        chuyenhang = 3,
        giaohang = 4,
        nhanhang = 5,
        
    }

}