using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wall.Models
{
    public class Message 
    {
        [Key]
        public int messageid { get; set; }
        [Required]
        [MinLength(2)]
        public string message { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int userid { get; set; }
        public User User { get; set; }
        public List<Comment> messagecomments { get; set; }
        // public User usermessages { get; set; }
        public Message()
        {
            messagecomments = new List<Comment>();
        }

    }
}