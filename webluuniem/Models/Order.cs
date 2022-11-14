using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string OrderName { get; set; }

        public decimal Price { get; set; }


        public int OrderStatus { get; set; }

        public DateTime CreatedDate { get; set; }



    }

}