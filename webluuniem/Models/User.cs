using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace webluuniem.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Slug { get; set; }

        public string Introduce { get; set;}

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Avatar { get; set; }

        public bool IsAdmin { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
    
    public class LoginModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class EditPasswordModel
    {
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string RePassword { get; set; }
    }
    public class UserModel:LoginModel
    {
        

        public string RePassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Introduce { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Avatar { get; set; }
    }
}