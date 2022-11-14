using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webluuniem.Models;

namespace webluuniem.Models
{
    public class HomeModel
    {
        public List<Product> ProductList { get; set; }

        public List<Category> CategoryList { get; set; } 
    }
}


