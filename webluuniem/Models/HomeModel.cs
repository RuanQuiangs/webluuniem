using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using webluuniem.Models;

namespace webluuniem.Models
{
    public class HomeModel
    {
        public List<Product> ProductList { get; set; }

        public List<Category> CategoryList { get; set; }

        public List<Post> PostList { get; set; }


    }

    public class PostModel
    {
        public List<Post> PostList { get; set; }

        public List<Comment> CommentList { get; set; }

        public List<User> UserList { get; set; }

        public Post Post { get; set; }

        public Comment Comment { get; set; }

    }
}


