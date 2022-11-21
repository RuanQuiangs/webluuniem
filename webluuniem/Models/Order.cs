using System;
using System.Collections.Generic;
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
        public Guid OrderID { get; set; }

        public int ProductID { get; set; }

        public Product Product { get; set; }

        public int UserID { get; set; }

        public UserModel User { get; set; }

        public string OrderName { get; set; }

        public decimal Price { get; set; }

        public string AdrressOrder { get; set; }

        public string PhoneOrder { get; set; }

        public OderStatus OrderStatus { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

    }

    public enum OderStatus
    {
        Dathang = 1,
        Xacnhan = 2,
        chuyenhang = 3,
        giaohang = 4,
        nhanhang = 5,
        
    }

}