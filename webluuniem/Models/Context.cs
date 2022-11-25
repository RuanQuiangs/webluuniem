using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace webluuniem.Models
{
    public class Context : DbContext
    {
        public Context() : base("name=WebluuniemdbConnection")
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categorys { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Post>Posts { get; set; }
     
        public DbSet<Comment> Comments { get; set; }

        public DbSet<ReView> ReViews { get; set; }

        public DbSet<Feedback>Feedbacks { get; set; }
    }
}