using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wall.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int userid { get; set; }
        [Required]
        [MinLength(3)]
        public string first_name { get; set; }
        [Required]
        [MinLength(3)]
        public string last_name { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<Message> postedmessages { get; set; }
        public List<Comment> postedcomments { get; set; }
        public User()
        {
            postedmessages = new List<Message>();
            postedcomments = new List<Comment>();
        }

    }

    public class LoginUser : BaseEntity
    {


        [Key]
        public long userid { get; set; }

        [Required]
        [EmailAddress]
        public string LogEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string LogPassword { get; set; }

    }

    public abstract class BaseEntity
    {

    }
}